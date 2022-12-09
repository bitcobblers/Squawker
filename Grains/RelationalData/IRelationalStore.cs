using Microsoft.EntityFrameworkCore;

namespace Grains.RelationalData
{
    public interface IRelationalStore
    {
        DbSet<HashTagLink> HashTagLinks { get; set; }
        
        DbSet<PostView> PostViews { get; set; }

        DbSet<PostView> ProfileViews { get; set; }

        DbSet<ProfileFriend> ProfileFollower { get; set; }

        DbSet<ProfileFriend> ProfileFriends { get; set; }
    }
}
