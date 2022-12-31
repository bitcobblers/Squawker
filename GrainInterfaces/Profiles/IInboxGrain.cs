using GrainInterfaces.States;

namespace GrainInterfaces.Profiles
{
    public interface IInboxGrain : IGrainWithGuidKey
    {
        Task DerectMesage();
        
        Task Notify();

        Task<Post[]> Get();

    }
}