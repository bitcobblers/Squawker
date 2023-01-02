using GrainInterfaces;
using GrainInterfaces.States;
using GrainInterfaces.Tags;

namespace FrontEnd.Posts.Query
{
    public class TagFeedQuery : FeedQuery
    {        
        public TagFeedQuery(Guid user, string tag) : base(user)
        {
            Tag = tag;
        }

        public string Tag { get; }

        public override IPostQueryable SelectQueryable(IClusterClient client)
        {
            return client.GetGrain<IHashTagGrain>(this.Tag);
        }
    }

    public abstract class FeedQuery : IPostQuery, IQueryableSelector        
    {
        protected FeedQuery(Guid user)
        {
            UserId = user;
        }

        public Guid UserId { get; set; }

        public abstract IPostQueryable SelectQueryable(IClusterClient client);
    }
}
