using GrainInterfaces.Model;

namespace GrainInterfaces.States
{
    [GenerateSerializer]
    public class ReactionTracker 
    {
        [Id(0)]
        public Dictionary<string, int> Data =new Dictionary<string, int>();
        
        public void Add(string reaction, int value)
        {
            if (!this.Data.ContainsKey(reaction)) { 
                this.Data.Add(reaction, 0);
            }

            this.Data[reaction] += value;
        }
    }

    [GenerateSerializer]
    public class Statistics  : EventJournaledState<Statistics, IStatisticsEvent>
    {
        [Id(0)]
        public ReactionTracker Reactions { get; set; } = new();
        [Id(1)]
        public double Comments { get; set; } = 0;
        [Id(2)]
        public double Views { get; set; } = 0; 
    }
}