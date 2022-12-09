using System.ComponentModel.DataAnnotations;

namespace Grains.RelationalData
{
    public class ProfileFollow : RelationalEvent
    {
        [Key]
        public Guid Profile { get; set; }

        [Key]
        public Guid Follower { get; set; }
    }
}
