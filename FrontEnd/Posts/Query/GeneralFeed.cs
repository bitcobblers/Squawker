using GrainInterfaces;

namespace FrontEnd.Posts.Query
{
    [GenerateSerializer]
    public abstract class FeedQuery : IFeedQuery, IFeedSelector        
    {
        protected FeedQuery(Guid user)
        {
            UserId = user;
        }

        [Id(0)]
        public Guid UserId { get; set; }

        public abstract IPostFeed GetQueryable(IClusterClient client);
    }
}
