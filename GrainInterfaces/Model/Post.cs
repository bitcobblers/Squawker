namespace GrainInterfaces.Model
{

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

        [Id(2)]
        public PostState State { get; set; } = PostState.None;

        [Id(3)]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        [Id(4)]
        public string Content { get; set; }


        #endregion

        #region Links

        [Id(5)]
        public Guid ReplyTo { get; set; } = Guid.Empty;

        [Id(6)]
        public Guid[] Replies { get; set; } = new Guid[0];

        #endregion
    }
}