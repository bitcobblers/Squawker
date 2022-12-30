using GrainInterfaces.States;

namespace GrainInterfaces.Model
{
    [GenerateSerializer]
    public class CreatePostRequest : Post, IPostEvent
    {
        public void Apply(Post state)
        {
            state.Author = this.Author;
            state.Content = this.Content;
            state.TimeStamp = this.TimeStamp;
            state.State = this.State;
        }
    }

    [GenerateSerializer]
    public class SimpleTextRequest : CreatePostRequest
    {
        public SimpleTextRequest() { }
        public SimpleTextRequest(string body, Guid user, Guid? reply = null)
        {
            this.Id = Guid.NewGuid();
            this.State = PostState.New;
            this.Author = user;
            this.Content = new[] { new PostContentSection()
            {
                ContentType = "Text",
                Body = body
            }};
            this.TimeStamp = DateTime.Now;
            this.ReplyTo = reply;
        }
    }
}
