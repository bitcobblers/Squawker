using GrainInterfaces.Model;

namespace GrainInterfaces
{

    public interface ICreatePostGrain: IGrainWithIntegerKey
    {
        Task<Post>  Create(RequestPost post);
    }
}