namespace GrainInterfaces
{
    public enum PostState
    {
        None = 0,
        New = 1,
        Flagged = 2,
        Approved = 3,
        Banned = 4
    }

    public interface IPostLink
    {
        Guid[] Posts { get; set; }
    }

    public interface IProfileLink
    {
        Guid Profile { get; set; }
    }
}