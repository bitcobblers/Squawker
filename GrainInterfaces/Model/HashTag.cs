namespace GrainInterfaces.Model
{
    [GenerateSerializer]
    public class HashTag
    {
        [Id(0)]
        public string Name { get; set; }

        [Id(1)]
        public Guid[] Posts { get; set; }
    }
}