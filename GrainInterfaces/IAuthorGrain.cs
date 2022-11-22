namespace GrainInterfaces
{
    public interface IAuthorGrain : Orleans.IGrainWithGuidKey
    {
        Task<Author> Get();
    }
}