using GrainInterfaces.Model;

namespace GrainInterfaces.States
{
    public interface IStatisticsEvent : IGrainEvent<Statistics> { }

    [GenerateSerializer]
    public class PostViewEvent : IStatisticsEvent
    {        
        public void Apply(Statistics state)
        {
            state.Views++;
        }
    }

    [GenerateSerializer]
    public class PositivePostReactionEvent : IStatisticsEvent
    {        
        public void Apply(Statistics state)
        {
            state.Points++;
        }
    }

    [GenerateSerializer]
    public class NegativePostReactionEvent : IStatisticsEvent
    {
        public void Apply(Statistics state)
        {
            state.Points--;
        }
    }

    [GenerateSerializer]
    public class PostCommentEvent : IStatisticsEvent
    {
        public void Apply(Statistics state)
        {
            state.Comments++;
        }
    }
}