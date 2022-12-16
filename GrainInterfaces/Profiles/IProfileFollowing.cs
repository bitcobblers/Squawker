namespace GrainInterfaces.Profiles
{
    public interface IProfileFollowing : IGrainWithGuidKey
    {
        Task FollowingUser(Guid userId);
    }
}