namespace GrainInterfaces
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        public Guid Author { get; set; } = Guid.Empty;


        public Guid ReplyTo { get; set; } = Guid.Empty;

        public Guid[] Replies { get; set; } = new Guid[0];
    }
}