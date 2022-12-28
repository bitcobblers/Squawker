using GrainInterfaces.Model;
using GrainInterfaces.Model.Index;
using GrainInterfaces.Posts;
using GrainInterfaces.States;
using Orleans.EventSourcing;
using Orleans.Providers;

namespace Grains.Posts
{       
    [StorageProvider(ProviderName = "Document")]
    public class PostGrain : EventJournaledGrain<Post, IGrainEvent<Post>>, IPostGrain
    {
        public async Task<Post> Get()
        {
            return State;
        }

        public Task Tag(HashTagLink[] tags)
        {
            if (tags is null)
            {
                throw new ArgumentNullException(nameof(tags));
            }

            throw new NotImplementedException();
        }


        public Task<Post> Post(CreatePostRequest post)
        {
            RaiseEvent(post);
            //var key = this.GetPrimaryKey();
            //store.Put(key, state);
            return this.Get();  
        }
    }
}