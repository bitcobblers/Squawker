using System.ComponentModel.DataAnnotations;

namespace GrainInterfaces.Model.Index
{
    public class ProfileFriend : RelationalEvent
    {
        [Key]
        public Guid Profile { get; set; }

        [Key]
        public Guid Friend { get; set; }
    }
}
