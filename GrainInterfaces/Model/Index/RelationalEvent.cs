namespace Grains.RelationalData
{
    [GenerateSerializer]
    public abstract class RelationalEvent
    {
        [Id(0)]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
