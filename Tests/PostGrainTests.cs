using GrainInterfaces.Model;
using GrainInterfaces.Posts;
using Grains.DocumentData;
using Microsoft.Extensions.DependencyInjection;
using Orleans.TestingHost;

namespace Tests
{

    public class TestSiloConfigurations : ISiloConfigurator
    {
        public void Configure(ISiloBuilder siloBuilder)
        {
            siloBuilder.AddMemoryGrainStorage("Document");
            siloBuilder.AddMemoryGrainStorage("Relational");
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
        public async Task SaysHelloCorrectly()
        {
            var post = _cluster.GrainFactory.GetGrain<ICreatePostGrain>(0);
            var createdPost = await post.Create(new GrainInterfaces.Model.Post { Content = new [] { TextSection.From("Hello, World") }, Author = Guid.NewGuid() });

            var postGrain = _cluster.GrainFactory.GetGrain<IPostGrain>(createdPost.Id);
            var greeting = await postGrain.Get();

            Assert.Equal("Hello, World", greeting.Content.First().Body);
        }
    }
}