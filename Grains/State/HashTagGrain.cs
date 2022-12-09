using GrainInterfaces.Model;
using GrainInterfaces.State;
using Grains.RelationalData;
using System.Collections;
using System.Collections.Concurrent;

namespace Grains.State
{
    public class FixedSizedQueue<T> : IEnumerable<T>
    {
        readonly ConcurrentQueue<T> queue = new ConcurrentQueue<T>();

        public int Size { get; private set; }

        public FixedSizedQueue(int size)
        {
            Size = size;
        }

        public void Enqueue(T obj)
        {
            queue.Enqueue(obj);

            while (queue.Count > Size)
            {
                T outObj;
                queue.TryDequeue(out outObj);
            }
        }

        public IEnumerator<T> GetEnumerator() => queue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => queue.GetEnumerator();
    }

    public class HashTagGrain : Grain, IHashTagGrain
    {
        private readonly IRelationalStore store;
        private readonly IClusterClient client;
        private readonly int takeLimit = 1000;
        private FixedSizedQueue<HastTagLink> links;

        public HashTagGrain(IRelationalStore store, IClusterClient client)
        {
            this.links = new FixedSizedQueue<HastTagLink>(this.takeLimit);
            this.store = store;
            this.client = client;
        }

        public override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            foreach (var link in this.store.HashTagLinks
                .Where(n => n.Name == this.IdentityString)
                .OrderByDescending(n => n.TimeStamp)
                .Take(takeLimit)
                .ToArray())
            {
                this.links.Enqueue(link);
            }

            return base.OnActivateAsync(cancellationToken);
        }
        
        public Task PushPost(Post post)
        {
            var link = new HastTagLink() { Name = this.IdentityString, Post = post.Id };
            this.links.Enqueue(link);
            this.store.HashTagLinks.Add(link);            
            
            return Task.CompletedTask;
        }

        public async Task<Post[]> GetPosts()
        {
            var tasks = this.links
                .Select(n => client.GetGrain<IPostGrain>(n.Post).GetContent())
                .ToArray();
            
            return await Task.WhenAll(tasks);
        }
    }
}