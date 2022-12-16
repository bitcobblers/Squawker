using GrainInterfaces.Model.Index;
using System.Runtime.CompilerServices;

namespace GrainInterfaces.Model
{
    public static class TextSection 
    {
        public static ContentSection From(string content)
        {
        return new ContentSection()
        {
            ContentType = "text",
            Body = content
        };
        }
    }

    public class PostBuilder : Post
    {
        public PostBuilder(params ContentSection[] sections) 
        {
            this.Content = sections;
        }
    }

    public class RequestPost : Post
    {

    }

    [GenerateSerializer]
    public class Post
    {
        #region Keys 

        [Id(0)]
        public Guid Id { get; set; } = Guid.Empty;
        [Id(1)]
        public Guid Author { get; set; } = Guid.Empty;
        
        #endregion

        #region Content 

        [Id(3)]
        public PostState State { get; set; } = PostState.None;

        [Id(4)]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        [Id(2)]
        public Guid? ReplyTo { get; set; }

        [Id(5)]
        public HashTagLink[] HashTags { get; set; } = new HashTagLink[0];
                
        [Id(6)]
        public ContentSection[] Content { get; set; }

        #endregion
    }

    [GenerateSerializer]
    public class ContentSection
    {
        [Id(0)]
        public string ContentType { get; set; }
        [Id(1)]
        public string Body { get; set; }
    }
}