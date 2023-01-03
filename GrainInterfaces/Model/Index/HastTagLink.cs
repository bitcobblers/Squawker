using System.ComponentModel.DataAnnotations;

namespace GrainInterfaces.Model.Index
{
    public enum HashTagLinkState
    {
        Created = 1,
        Moderated = 2,
        Unauthorized = 4,
        Deleted = 8,
    }

    [GenerateSerializer]
    public class HashTagLink
    {        
        [Id(0)]
        public string Name { get; set; }
     
        [Id(1)]
        public Guid PostId { get; set; }
                
        [Id(2)]
        public HashTagLinkState State { get; set; }
    }
}
