using System.ComponentModel.DataAnnotations;

namespace GrainInterfaces.Model.Index
{
    public class ProfileView : RelationalEvent
    {
        [Key]
        public Guid Profile { get; set; }

        [Key]
        public Guid Visitor { get; set; }
    }
}
