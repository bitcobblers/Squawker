using GrainInterfaces.Model;

namespace GrainInterfaces.Tags
{
    public interface ICreateHashTagsGrain : IGrainWithIntegerKey
    {
        Task Create(Post post);
    }
}