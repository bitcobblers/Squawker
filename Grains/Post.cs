using GrainInterfaces;
using System.Runtime.CompilerServices;

namespace Grains
{
    public class PostGrain : Orleans.Grain<Post>, IPostGrain
    {
        public override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return base.OnActivateAsync(cancellationToken);
        }


        public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
        {
            return base.OnDeactivateAsync(reason, cancellationToken);
        }

        public async Task<Post> GetContent()
        {
            return this.State;
        }
    }
}