using GrainInterfaces.Model;
using GrainInterfaces.State;
using Orleans;
using Orleans.EventSourcing;
using Orleans.Providers;

namespace Grains.State
{

    [StorageProvider(ProviderName = "File")]
    public class ProfileGrain : JournaledGrain<Profile, IGrainEvent<Profile>>, IProfileGrain
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
            return ;
        }
    }
}