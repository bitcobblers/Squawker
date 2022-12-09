using GrainInterfaces.Model;
using Grains.RelationalData;

namespace GrainInterfaces.State
{

    public interface IPostGrain : IGrainWithGuidKey
    {
        Task<Post> GetContent();
        Task LinkHashTags(HashTagLink[] tags);
        Task Post(Post post);
    }
}