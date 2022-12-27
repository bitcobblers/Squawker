using GrainInterfaces.Model;

namespace GrainInterfaces.States
{
    [GenerateSerializer]
    public class HashTag : IPostLink
    {
        [Id(0)]
        public string Name { get; set; }

        [Id(1)]
        public Guid[] Posts { get; set; }
    }
}