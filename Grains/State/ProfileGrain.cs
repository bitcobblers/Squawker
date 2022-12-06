using GrainInterfaces.Model;
using GrainInterfaces.State;

namespace Grains.State
{
    public class ProfileGrain : Grain, IProfileGrain
    {
        private Profile State = null;
        public async Task Follow()
        {
            return;
        }

        public async Task Friend()
        {
            return;
        }

        public async Task<Profile> Get()
        {
            return State;
        }

        public async Task SetPost(Post post)
        {
            return ;
        }
    }
}