namespace GrainInterfaces
{

    public class Post
    {        
        #region Keys 
     
        public Guid Id { get; set; } = Guid.Empty;
        public Guid Author { get; set; } = Guid.Empty;

        #endregion

        #region Content 

        public PostState State { get; set; } = PostState.None;

        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        public string Content { get; set; } = string.Empty;


        #endregion

        #region Links

        public Guid ReplyTo { get; set; } = Guid.Empty;

        public Guid[] Replies { get; set; } = new Guid[0];

        #endregion
    }
}