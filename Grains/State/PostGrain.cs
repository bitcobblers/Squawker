using GrainInterfaces.Model;
using GrainInterfaces.State;
using Grains.DocumentData;

namespace Grains.State
{
    public class PostGrain : Grain, IPostGrain
    {
        private Post? state;
        private readonly IDocumentStore store;

        public PostGrain(IDocumentStore store)
        {
            this.store = store;
        }        

        public override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            var key = this.GetPrimaryKey();
            this.state = store.Get<Post>(key);
            if (this.state == null)
            {
                this.state = new Post() { Id = key };
                store.Put(key, state);
            }            
            return base.OnActivateAsync(cancellationToken);
        }


        public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
        {
            var key = this.GetPrimaryKey();
            this.store.Put(key, this.state);

            return base.OnDeactivateAsync(reason, cancellationToken);
        }

        public async Task<Post> GetContent()
        {
            return state;
        }

        public Task Post(Post post)
        {
            throw new NotImplementedException();
        }
    }
}