using GrainInterfaces.States;

namespace GrainInterfaces.Profiles
{

    public interface IProfileGrain : IGrainWithGuidKey
    {
        Task<Profile> Get();
    }
}