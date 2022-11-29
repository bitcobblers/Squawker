using Microsoft.Extensions.FileProviders;
using System.Text.Json;

namespace Grains.DocumentData
{
    public class JsonDocumentStore : IDocumentStore            
    {
        private readonly IFileName namer;
        private readonly IFileWriter fileProvider;

        public JsonDocumentStore(IFileWriter fileProvider, IFileName namer = null) 
        {
            this.namer = namer ?? new JsonFileName();
            this.fileProvider = fileProvider;
        }

        public void Put<TType>(Guid id, TType model) where TType : class
        {
            this.fileProvider.Write(namer.Get<TType>(id), JsonSerializer.Serialize(model));           
        }

        public TType Get<TType>(Guid id) where TType : class
        {
            var name = this.namer.Get<TType>(id);
            var fileInfo = this.fileProvider.GetFileInfo(name);
            if (fileInfo.Exists)
            {
                return JsonSerializer.Deserialize<TType>(fileInfo.CreateReadStream());
            }

            return default;
        }

        public TType[] Get<TType>(Guid[] ids) where TType : class
        {
            return ids?
                .Select(id => this.Get<TType>(id))
                .Where(result => result != null)
                .AsParallel()                
                .ToArray() ?? new TType[0];
        }
    }
}
