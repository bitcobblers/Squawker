using GrainInterfaces.States;

namespace GrainInterfaces
{    
    public interface IQueryableSelector : IPostQuery
    {
        IPostQueryable GetQueryable(IClusterClient client);                
    }

    public interface IPostQuery
    {
        Guid UserId { get; }
    }
}