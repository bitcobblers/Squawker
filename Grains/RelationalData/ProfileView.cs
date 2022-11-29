using System.ComponentModel.DataAnnotations;

namespace Grains.RelationalData
{
    public class ProfileView : RelationalEvent
    {
        [Key]
        public Guid Profile { get; set; }

        [Key]
        public Guid Visitor { get; set; }        
    }
}
