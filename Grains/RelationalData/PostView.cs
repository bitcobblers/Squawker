using System.ComponentModel.DataAnnotations;

namespace Grains.RelationalData
{
    public class PostView : RelationalEvent
    {
        [Key]
        public Guid Post { get; set; }

        [Key]
        public Guid Visitor { get; set; }            
    }
}
