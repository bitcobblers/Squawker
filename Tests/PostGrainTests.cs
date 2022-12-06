﻿using GrainInterfaces;
using GrainInterfaces.State;
using Grains.DocumentData;
using Microsoft.Extensions.DependencyInjection;
using Orleans.TestingHost;

namespace Tests
{

    public class TestSiloConfigurations : ISiloConfigurator
    {
        public void Configure(ISiloBuilder siloBuilder)
        {
            siloBuilder.AddMemoryGrainStorage("File");
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
            var createdPost = await post.Create(new GrainInterfaces.Model.Post { Content = "Hello, World" }, Guid.NewGuid());

            var postGrain = _cluster.GrainFactory.GetGrain<IPostGrain>(createdPost.Id);
            var greeting = await postGrain.GetContent();

            Assert.Equal("Hello, World", greeting.Content);
        }
    }
}