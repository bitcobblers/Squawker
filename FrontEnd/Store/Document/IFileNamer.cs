namespace FrontEnd.Store.DocumentData
{
    public interface IFileNamer
    {
        string Get<TType>(string clusterId, Guid grainId);
    }
}
