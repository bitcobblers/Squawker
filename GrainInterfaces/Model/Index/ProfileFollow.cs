using System.ComponentModel.DataAnnotations;

namespace GrainInterfaces.Model.Index
{
    public class ProfileFollow : RelationalEvent
    {
        [Key]
        public Guid Profile { get; set; }

        [Key]
        public Guid Follower { get; set; }
    }
}
