namespace Grains.DocumentData
{
    public class JsonFileName : IFileNamer
    {    
        public string Get<TType>(string clusterId, Guid grainId)
        {
            return $"{clusterId}.{grainId}.{nameof(TType)}.json";
        }
    }
}
