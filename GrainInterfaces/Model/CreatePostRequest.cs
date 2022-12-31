using GrainInterfaces.Model.Index;
using GrainInterfaces.States;

namespace GrainInterfaces.Model
{
    [GenerateSerializer]
    public class CreatePostRequest : Post, IPostEvent
    {
        public void Apply(Post state)
        {
            state.Id = this.Id;
            state.Author = this.Author;
            state.Content = this.Content;
            state.TimeStamp = this.TimeStamp;
            state.State = this.State;
        }
    }


    [GenerateSerializer]
    public class UpdatePostRequest : IPostEvent
    {
        [Id(0)]
        public bool Orginal { get; set;  } = false;

        [Id(1)]
        public PostState? State { get; set; } = PostState.None;


        [Id(2)]
        public DateTime? TimeStamp { get; set; } = DateTime.UtcNow;


        [Id(3)]
        public HashTagLink[] HashTags { get; set; } = new HashTagLink[0];


        [Id(4)]
        public PostContentSection[] Content { get; set; } = new PostContentSection[0];

        public void Apply(Post state)
        {
            state.Original = state.Original && this.Orginal;

            state.TimeStamp = this.TimeStamp.HasValue ? this.TimeStamp.Value : state.TimeStamp;
            state.State = this.State.HasValue ? this.State.Value : state.State;            
            
            if (this.Content.Any())
            {
                state.Content = this.Content;
            }

            if (this.HashTags.Any())
            {
                state.HashTags = this.HashTags;
            }
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