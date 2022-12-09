using GrainInterfaces.Model;
using GrainInterfaces.State;
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
            state.State = GrainInterfaces.PostState.New;            
        }
    }

    [StorageProvider(ProviderName = "Document")]
    public class PostGrain : JournaledGrain<Post, IGrainEvent<Post>>, IPostGrain
    {                     
        public async Task<Post> GetContent()
        {
            return this.State;
        }

        public Task LinkHashTags(HashTagLink[] tags)
        {
            throw new NotImplementedException();
        }

        public Task Post(Post post)
        {
            RaiseEvent(new NewPostEvent(post));
            
            //var key = this.GetPrimaryKey();
            //store.Put(key, state);
            return Task.CompletedTask;

        }
    }
}