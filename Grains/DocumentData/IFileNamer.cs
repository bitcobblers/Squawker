namespace Grains.DocumentData
{
    public interface IFileNamer
    {
        string Get<TType>(string clusterId, Guid grainId);
    }
}
