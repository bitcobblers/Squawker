using System.ComponentModel.DataAnnotations;

namespace Grains.RelationalData
{
    public enum HashTagLinkState
    {        
        Created = 1,
        Moderated = 2,
        Unauthorized = 4,
        Deleted = 8,
    }

    [GenerateSerializer]
    public class HashTagLink : RelationalEvent
    {
        [Key]
        [Id(0)]
        public string Name { get; set; }
        
        [Key]
        [Id(1)]
        public Guid Post { get; set; }

        [Id(2)]
        public HashTagLinkState State { get; set; }
    }
}
