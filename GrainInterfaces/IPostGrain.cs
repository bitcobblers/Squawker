namespace GrainInterfaces
{
    public interface  IPostGrain : Orleans.IGrainWithGuidKey
    {
        Task<Post> GetContent();

        Task Post(Post post);
    }
}