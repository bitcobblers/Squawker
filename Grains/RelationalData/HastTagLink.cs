using System.ComponentModel.DataAnnotations;

namespace Grains.RelationalData
{
    public class HastTagLink : RelationalEvent
    {
        [Key]
        public string Name { get; set; }
        
        [Key]
        public Guid Post { get; set; }                
    }
}
