using GrainInterfaces.Model;

namespace GrainInterfaces.State
{
    public interface IHashTagGrain : IGrainWithStringKey
    {
        Task<Post[]> GetPosts();
    }
}