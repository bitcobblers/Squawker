using GrainInterfaces.Model;
using GrainInterfaces.State;
using Grains.RelationalData;

namespace Grains.State
{
    public class HashTagGrain : Grain<HashTag>, IHashTagGrain
    {
        private readonly IRelationalStore store;

        public HashTagGrain(IRelationalStore store)
        {
            this.store = store;
        }

        public override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return base.OnActivateAsync(cancellationToken);
        }

        public Task<Post[]> GetPosts()
        {
            throw new NotImplementedException();
        }
    }
}