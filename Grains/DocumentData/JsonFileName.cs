namespace Grains.DocumentData
{
    public class JsonFileName : IFileName
    {        
        public string Get<TType>(Guid id) where TType : class
        {
            return $"{id}.{typeof(TType).Name}.json";
        }
    }
}
