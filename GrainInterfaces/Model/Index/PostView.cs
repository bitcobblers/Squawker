using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace GrainInterfaces.Model.Index
{   
    public class HashTagIndex
    {
        public string Name { get; set; }        
        
        public HashTagLinkState State { get; set; }

        public Guid PostIndexId { get; set; }

        public PostIndex PostIndex { get; set; }
    }

    public class PostIndex : RelationalEvent
    {        
        public Guid PostIndexId { get; set; }

        public Guid AuthorIndexId { get; set; }

        public Guid? ReplyTo { get; set; }
    }
}
