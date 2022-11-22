namespace GrainInterfaces
{
    public interface ICreatePostGrain
    {
        Task<Post>  Create(Post post, Guid author);
    }
}