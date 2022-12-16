using GrainInterfaces.Model;
using GrainInterfaces.Posts;
using Grains.RelationalData;
using Orleans.EventSourcing;
using Orleans.Providers;

namespace Grains.State
{
    public interface IGrainEvent<TType>
    {
        void Apply(TType state);
    }

    public class NewPostEvent : IGrainEvent<Post>
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

    [StorageProvider(ProviderName = "Document")]
    public class PostGrain : JournaledGrain<Post, IGrainEvent<Post>>, IPostGrain
    {                     
        public async Task<Post> Get()
        {
            return this.State;
        }

        public Task Tag(HashTagLink[] tags)
        {
            if (tags is null)
            {
                throw new ArgumentNullException(nameof(tags));
            }

            throw new NotImplementedException();
        }

        
        public Task<Post> Post(RequestPost post)
        {
            RaiseEvent(new NewPostEvent(post));

            //var key = this.GetPrimaryKey();
            //store.Put(key, state);
            return Task.CompletedTask;
        }
    }
}