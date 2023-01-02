using GrainInterfaces.States;

namespace GrainInterfaces
{    
    public interface IQueryableSelector : IPostQuery
    {
        IPostQueryable SelectQueryable(IClusterClient client);                
    }

    public interface IPostQuery
    {
        Guid UserId { get; }
    }
}