namespace FrontEnd.Posts.Query
{
    [GenerateSerializer]
    public class PublicFeedQuery : TagFeedQuery
    {
        public PublicFeedQuery(Guid user) : base(user, string.Empty)
        {
        }
    }
}
