using GrainInterfaces.States;

namespace GrainInterfaces
{
    public interface IFeedCondition {         
    }

    public interface IFeedQuery
    {
        Guid UserId { get; }

        IFeedCondition[] Conditions { get; }
    }
}