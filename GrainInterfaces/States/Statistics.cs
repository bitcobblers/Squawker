using GrainInterfaces.Model;

namespace GrainInterfaces.States
{
    [GenerateSerializer]
    public class Statistics  : EventJournaledState<Statistics, IStatisticsEvent>
    {
        [Id(0)]
        public double Points { get; set; } = 0;
        [Id(1)]
        public double Comments { get; set; } = 0;
        [Id(2)]
        public double Views { get; set; } = 0; 
    }
}