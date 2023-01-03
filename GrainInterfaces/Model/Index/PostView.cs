using System.ComponentModel.DataAnnotations;

namespace GrainInterfaces.Model.Index
{
    public class PostIndex
    {
        [Key]
        public Guid Post { get; set; }

        [Key]
        public Guid Visitor { get; set; }
    }
}
