namespace GrainInterfaces
{
    public interface IAuthorGrain : Orleans.IGrainWithGuidKey
    {
        Task<Author> Get();

        Task Follow();

        Task Friend();

        Task SetPost(Post post);
    }
}