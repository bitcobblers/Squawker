namespace GrainInterfaces.Profiles
{
    public interface IProfileFollowers : IGrainWithGuidKey
    {
        Task FollowedByUser(Guid userId);
    }
}