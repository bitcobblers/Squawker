using GrainInterfaces.Model;
using Grains.RelationalData;

namespace GrainInterfaces.State
{
    public interface IHashTagGrain : IGrainWithStringKey
    {
        Task<HashTagLink> Link(Post post);
        Task<Guid[]> Posts();
    }
}