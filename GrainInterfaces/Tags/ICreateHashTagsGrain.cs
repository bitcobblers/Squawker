using GrainInterfaces.States;

namespace GrainInterfaces.Tags
{
    public interface ICreateHashTagsGrain : IGrainWithIntegerKey
    {
        Task Create(Post post);
    }
}