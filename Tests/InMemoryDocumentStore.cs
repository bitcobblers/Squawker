using Grains.DocumentData;

namespace Tests
{
    public class InMemoryDocumentStore : IDocumentStore
    {
        public Dictionary<string, object> Documents { get; set; } = new Dictionary<string, object>();
        private readonly IFileName namer = new JsonFileName();
        public TType? Get<TType>(Guid id) where TType : class
        {
            var key = this.namer.Get<TType>(id);
            return this.Documents.ContainsKey(key)  
                ? this.Documents[key] as TType
                : default;   
        }

        public TType[] Get<TType>(Guid[] ids) where TType : class
        {
            return ids.Select(id => this.Get<TType>(id)).ToArray();
        }

        public void Put<TType>(Guid id, TType model) where TType : class
        {
            var key = this.namer.Get<TType>(id);
            if (!this.Documents.ContainsKey(key))
            {
                this.Documents.Add(key, null);
            }
            this.Documents[key] = model;
        }
    }
}