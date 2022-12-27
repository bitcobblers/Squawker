using GrainInterfaces.Model;

namespace GrainInterfaces.States
{
    public interface IStatisticsEvent : IGrainEvent<Statistics> { }

    public class PostViewEvent : IStatisticsEvent
    {        
        public void Apply(Statistics state)
        {
            state.Views++;
        }
    }

    public class PositivePostReactionEvent : IStatisticsEvent
    {        
        public void Apply(Statistics state)
        {
            state.Points++;
        }
    }

    public class NegativePostReactionEvent : IStatisticsEvent
    {
        public void Apply(Statistics state)
        {
            state.Points--;
        }
    }

    public class PostCommentEvent : IStatisticsEvent
    {
        public void Apply(Statistics state)
        {
            state.Comments++;
        }
    }
}