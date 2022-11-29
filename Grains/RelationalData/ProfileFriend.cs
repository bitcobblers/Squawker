using System.ComponentModel.DataAnnotations;

namespace Grains.RelationalData
{
    public class ProfileFriend : RelationalEvent
    {
        [Key]
        public Guid Profile { get; set; }

        [Key]
        public Guid Friend { get; set; }
    }
}
