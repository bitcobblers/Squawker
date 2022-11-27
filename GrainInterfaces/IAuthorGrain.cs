namespace GrainInterfaces
{
    public interface IAuthorGrain : Orleans.IGrainWithGuidKey
    {
        Task<Profile> Get();

        Task Follow();

        Task Friend();

        Task SetPost(Post post);
    }
}