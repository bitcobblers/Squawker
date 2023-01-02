using GrainInterfaces.Model;
using GrainInterfaces.Posts;
using Orleans.TestingHost;
using Orleans.Core;
using Orleans.Runtime;

namespace Tests
{

    public class TestSiloConfigurations : ISiloConfigurator
    {
        public void Configure(ISiloBuilder siloBuilder)
        {
            siloBuilder.AddMemoryGrainStorage("Document");
            siloBuilder.AddMemoryGrainStorage("Document");            

        }   
    }

    public class ClusterFixture : IDisposable
    {
        public ClusterFixture()
        {
            var builder = new TestClusterBuilder();
            
            builder.AddSiloBuilderConfigurator<TestSiloConfigurations>();
            Cluster = builder.Build();            
            Cluster.Deploy();
        }

        public void Dispose() => Cluster.StopAllSilos();

        public TestCluster Cluster { get; }
    }

    [CollectionDefinition(ClusterCollection.Name)]
    public class ClusterCollection : ICollectionFixture<ClusterFixture>
    {
        public const string Name = "ClusterCollection";
    }

    [Collection(ClusterCollection.Name)]
    public class PostGrainTests
    {
        private readonly TestCluster _cluster;

        public PostGrainTests(ClusterFixture fixture)
        {
            _cluster = fixture.Cluster;
        }

        [Fact]
        public async Task CanCreateATestPost()
        {
            var post = _cluster.GrainFactory.GetGrain<ICreatePostGrain>(0);
            var createdPost = await post.Create(new SimpleTextRequest("Hello, World", Guid.NewGuid()));

            var postGrain = _cluster.GrainFactory.GetGrain<IPostGrain>(createdPost.Id);
            var greeting = await postGrain.Get();

            Assert.Equal("Hello, World", greeting.Content?.First()?.Body);
        }

        [Fact]
        public async Task GettingThePostIncrementsTheViewStatstics()
        {
            var post = _cluster.GrainFactory.GetGrain<ICreatePostGrain>(0);
            
            var createdPost = await post.Create(new SimpleTextRequest("Hello, World", Guid.NewGuid()));

            var stats = _cluster.GrainFactory.GetGrain<IPostStatisticsGrain>(createdPost.Id);
            var postGrain = _cluster.GrainFactory.GetGrain<IPostGrain>(createdPost.Id);
            var greeting = await postGrain.Get();
            var views = await stats.Get();

            Assert.Equal(1, views.Views);
        }

        [Fact]
        public async Task ReplysIncrementsTheCommentStatstics()
        {
            var post = _cluster.GrainFactory.GetGrain<ICreatePostGrain>(0);
            var reply = _cluster.GrainFactory.GetGrain<ICreateReplyGrain>(0);

            var createdPost = await post.Create(new SimpleTextRequest("Hello, World", Guid.NewGuid()));
            await reply.ReplyTo(createdPost.Id, Guid.NewGuid());

            var stats = _cluster.GrainFactory.GetGrain<IPostStatisticsGrain>(createdPost.Id);            
            var views = await stats.Get();

            Assert.Equal(1, views.Comments);
        }


        [Fact]
        public async Task CreatingAPostIdentifiesHashTags()
        {
            var post = _cluster.GrainFactory.GetGrain<ICreatePostGrain>(0);                        

            var createdPost = await post.Create(new SimpleTextRequest("Hello, #World", Guid.NewGuid()));                        
            Assert.Equal(1, createdPost.HashTags.Count());
            Assert.Equal("hashtag/World", createdPost.HashTags[0].Name);
        }
    }
}