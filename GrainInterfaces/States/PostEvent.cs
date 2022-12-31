using GrainInterfaces.Model;

namespace GrainInterfaces.States
{
    public interface IPostEvent  : IGrainEvent<Post> { }

    public class NewCommentEvent : IPostEvent
    {
        public void Apply(Post state)
        {            
        }
    }

    public class NewPostEvent : IPostEvent
    {
        private Post post;

        public NewPostEvent(Post post)
        {
            this.post = post;
        }

        public void Apply(Post state)
        {
            state.Author = post.Author;
            state.Content = post.Content;
            state.TimeStamp = post.TimeStamp;
            state.State = PostState.New;
        }
    }

}