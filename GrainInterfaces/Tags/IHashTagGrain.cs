using GrainInterfaces.Model.Index;
using GrainInterfaces.States;

namespace GrainInterfaces.Tags
{
    public interface IHashTagGrain : IPostQueryable, IGrainWithStringKey
    {
        Task<HashTagLink> Link(Post post);        
    }
}