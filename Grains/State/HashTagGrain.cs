using GrainInterfaces.Model;
using GrainInterfaces.State;

namespace Grains.State
{
    public class HashTagGrain : Grain<HashTag>, IHashTagGrain
    {
        public Task<Post[]> GetPosts()
        {
            throw new NotImplementedException();
        }
    }
}