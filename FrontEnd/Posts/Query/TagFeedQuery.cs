using GrainInterfaces;
using GrainInterfaces.Tags;

namespace FrontEnd.Posts.Query
{
    [GenerateSerializer]
    public class TagFeedQuery : FeedQuery
    {        
        public TagFeedQuery(Guid user, string tag) : base(user)
        {
            Tag = tag;
        }

        [Id(0)]
        public string Tag { get; }

        public override IPostFeed GetQueryable(IClusterClient client)
        {
            return client.GetGrain<IHashTagGrain>(this.Tag);
        }
    }
}
