using GrainInterfaces.Model;
using GrainInterfaces.Model.Index;

namespace GrainInterfaces.Tags
{
    public interface IHashTagGrain : IGrainWithStringKey
    {
        Task<HashTagLink> Link(Post post);
        Task<Guid[]> Posts();
    }
}