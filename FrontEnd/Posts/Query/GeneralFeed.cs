using GrainInterfaces;

namespace FrontEnd.Posts.Query
{
    [GenerateSerializer]
    public abstract class FeedQuery : IPostQuery, IQueryableSelector        
    {
        protected FeedQuery(Guid user)
        {
            UserId = user;
        }

        [Id(0)]
        public Guid UserId { get; set; }

        public abstract IPostQueryable GetQueryable(IClusterClient client);
    }
}
