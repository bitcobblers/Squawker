using GrainInterfaces.Model;
using GrainInterfaces.Posts;

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
    public class PostReactionEvent : IStatisticsEvent
    {
        public PostReactionEvent()
        {
        }

        public PostReactionEvent(IReaction reaction)
        {
            Reaction = reaction;
        }

        [Id(0)]
        public IReaction? Reaction { get; set; }

        public void Apply(Statistics state)
        {
            if (this.Reaction != null)
            {
                state.Reactions.Add(this.Reaction.Categorty, this.Reaction.Value);
            }
        }
    }

    [GenerateSerializer]
    public class NegativePostReactionEvent : IStatisticsEvent
    {
        public void Apply(Statistics state)
        {
            state.Reactions.Add("Points", -1);
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