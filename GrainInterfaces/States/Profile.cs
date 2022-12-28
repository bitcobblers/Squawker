using GrainInterfaces.Model;

namespace GrainInterfaces.States
{
    public interface IProfileEvent : IGrainEvent<Profile>
    {
    }

    [GenerateSerializer]
    public class Profile : EventJournaledState<Profile, IProfileEvent>
    {
        [Id(0)]
        public Guid AuthorId { get; set; } = Guid.Empty;

        [Id(1)]
        public string Name { get; set; } = string.Empty;

        [Id(2)]
        public string Picture { get; set; } = string.Empty;

        [Id(3)]
        public string Description { get; set; } = string.Empty;

        [Id(4)]
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}