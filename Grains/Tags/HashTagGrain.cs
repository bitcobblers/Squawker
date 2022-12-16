using GrainInterfaces.Model;
using GrainInterfaces.Model.Index;
using GrainInterfaces.Tags;
using Grains.RelationalData;
using System.Collections;
using System.Collections.Concurrent;

namespace Grains.Tags
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
        private FixedSizedQueue<HashTagLink> links;

        public HashTagGrain(IRelationalStore store, IClusterClient client)
        {
            links = new FixedSizedQueue<HashTagLink>(takeLimit);
            this.store = store;
            this.client = client;
        }

        public override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            foreach (var link in store.HashTagLinks
                .Where(n => n.Name == IdentityString)
                .OrderByDescending(n => n.TimeStamp)
                .Take(takeLimit)
                .ToArray())
            {
                links.Enqueue(link);
            }

            return base.OnActivateAsync(cancellationToken);
        }


        public async Task<HashTagLink> Link(Post post)
        {
            var link = new HashTagLink() { Name = IdentityString, Post = post.Id, ProfileId = post.Author };

            await store.HashTagLinks.AddAsync(link);
            links.Enqueue(link);

            return link;
        }

        public Task<Guid[]> Posts() => Task.FromResult(links
            .Select(n => n.Post)
            .ToArray());
    }
}