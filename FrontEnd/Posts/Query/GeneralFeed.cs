using GrainInterfaces;

namespace FrontEnd.Posts.Query
{
    [GenerateSerializer]
    public abstract class FeedQuery : IFeedQuery, IFeedSelector        
    {
        protected FeedQuery(Guid user, params IFeedCondition[] conditions)
        {
            UserId = user;
            Conditions = conditions;
        }

        [Id(0)]
        public Guid UserId { get; set; }

        [Id(1)]
        public IFeedCondition[] Conditions { get; set; } = new IFeedCondition[0];

        public abstract IPostFeed GetFeed(IClusterClient client);
    }
}
