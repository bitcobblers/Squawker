using System.ComponentModel.DataAnnotations;

namespace GrainInterfaces.Model.Index
{
    public class PostView : RelationalEvent
    {
        [Key]
        public Guid Post { get; set; }

        [Key]
        public Guid Visitor { get; set; }
    }
}
