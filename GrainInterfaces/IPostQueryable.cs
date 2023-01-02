using GrainInterfaces.States;

namespace GrainInterfaces
{
    public interface IPostQueryable
    {
        Task<Guid[]> Query(IPostQuery request);
    }
}