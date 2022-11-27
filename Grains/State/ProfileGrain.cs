using GrainInterfaces;

namespace Grains.State
{
    public class ProfileGrain : Grain<Author>, IAuthorGrain
    {
        public async Task Follow()
        {
            return;
        }

        public async Task Friend()
        {
            return;
        }

        public async Task<Author> Get()
        {
            return State;
        }

        public async Task SetPost(Post post)
        {
            return;
        }
    }
}