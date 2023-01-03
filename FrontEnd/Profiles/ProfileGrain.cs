using GrainInterfaces;
using GrainInterfaces.Model;
using GrainInterfaces.Profiles;
using GrainInterfaces.States;
using Orleans;


using Orleans.Providers;

namespace Grains.Profiles
{
   
    public class ProfilePostGrain : IProfilePostsGrain
    {
        public Task<Guid[]> Query(IFeedQuery request)
        {
            return Task.FromResult(new Guid[0]);
        }

        public Task PostCreated(Post post)
        {
            Console.WriteLine("Test");
            return Task.CompletedTask;
        }
    }

    [StorageProvider(ProviderName = "Memory")]
    public class ProfileGrain : EventGrain<Profile, IGrainEvent<Profile>>, IProfileGrain
    {
        public async Task Follow()
        {
            return;
        }

        public async Task Friend()
        {
            return;
        }

        public async Task<Profile> Get()
        {
            return State;
        }

        public async Task SetPost(Post post)
        {
            return;
        }
    }
}