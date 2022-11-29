namespace Grains.RelationalData
{
    public abstract class RelationalEvent
    {
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
