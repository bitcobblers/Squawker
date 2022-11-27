namespace GrainInterfaces.Model
{
    [GenerateSerializer]
    public class Profile
    {
        public Guid AuthorId { get; set; } = Guid.Empty;

        public string Name { get; set; } = string.Empty;

        public string Picture { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}