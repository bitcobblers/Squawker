namespace GrainInterfaces.Posts
{
    public interface ICreateReplyGrain : IGrainWithIntegerKey
    {
        Task ReplyTo(Guid? message, Guid reply);
    }
}