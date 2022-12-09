using GrainInterfaces.Model;
using Grains.RelationalData;

namespace GrainInterfaces.State
{
    public interface IHashTagGrain : IGrainWithStringKey
    {
        Task<HastTagLink> Link(Post post);
        Task<Post[]> GetPosts();
    }
}