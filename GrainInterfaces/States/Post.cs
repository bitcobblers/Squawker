using GrainInterfaces.Model;
using GrainInterfaces.Model.Index;

namespace GrainInterfaces.States
{
    [GenerateSerializer]
    public class Post : EventJournaledState<Post, IPostEvent>
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
        public PostContentSection[] Content { get; set; }

        #endregion
    }
}