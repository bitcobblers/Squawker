﻿using GrainInterfaces.Model;
using GrainInterfaces.State;

namespace Grains.State
{
    public class PostGrain : Grain<Post>, IPostGrain
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
            return State;
        }

        public Task Post(Post post)
        {
            throw new NotImplementedException();
        }
    }
}