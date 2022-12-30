using Microsoft.Extensions.Options;
using Orleans.Configuration;
using Orleans.Runtime;
using Orleans.Storage;
using System.Text.Json;

namespace Grains.DocumentData
{

    public class FileGrainStorage
        : IGrainStorage, ILifecycleParticipant<ISiloLifecycle>
    {
        private readonly string _storageName;
        private readonly FileGrainStorageOptions _options;
        private readonly ClusterOptions _clusterOptions;        

        public FileGrainStorage(
            string storageName,
            FileGrainStorageOptions options,
            IOptions<ClusterOptions> clusterOptions)
        {
            _storageName = storageName;
            _options = options;
            _clusterOptions = clusterOptions.Value;            

    }

        private Task Init(CancellationToken ct)
        {
            // Settings could be made configurable from Options.
            
            return Task.CompletedTask;
        }

        public Task ReadStateAsync<T>(string stateName, GrainId grainId, IGrainState<T> grainState)            
        {                       
            var filename = this._options.FileNamer.Get<T>(this._clusterOptions.ServiceId, grainId.GetGuidKey());            
            var fileInfo = this._options.RootDirectory.GetFileInfo(filename);

            if (fileInfo == null || !fileInfo.Exists) {
                grainState.State = Activator.CreateInstance<T>();
            }
            else
            {
                var stream = fileInfo?.CreateReadStream() ?? new MemoryStream();
                grainState.State = JsonSerializer.Deserialize<T>(stream);
            }            

            grainState.ETag = fileInfo.LastModified.ToString();
            return Task.CompletedTask;
            
        }

        public Task WriteStateAsync<T>(string stateName, GrainId grainId, IGrainState<T> grainState)
        {

            var filename = this._options.FileNamer.Get<T>(this._clusterOptions.ServiceId, grainId.GetGuidKey());
            var fileInfo = this._options.RootDirectory.GetFileInfo(filename);


            if (fileInfo.Exists && fileInfo.LastModified.ToString() != grainState.ETag)
            {
                throw new InconsistentStateException(
                    $"Version conflict (WriteState): ServiceId={_clusterOptions.ServiceId} " +
                    $"ProviderName={_storageName} GrainType={nameof(T)} " +
                    $"GrainReference={grainId.GetGuidKey()}.");
            }

                        
            this._options.RootDirectory.Write(filename, JsonSerializer.Serialize(grainState.State));
            var lastModified = this._options.RootDirectory.GetFileInfo(filename).LastModified.ToString();
            grainState.ETag = lastModified;

            return Task.CompletedTask;
        }

        public Task ClearStateAsync<T>(string stateName, GrainId grainId, IGrainState<T> grainState)
        {
            var filename = this._options.FileNamer.Get<T>(this._clusterOptions.ServiceId, grainId.GetGuidKey());
            var fileInfo = this._options.RootDirectory.GetFileInfo(filename);


            if (fileInfo.Exists && fileInfo.LastModified.ToString() != grainState.ETag)
            {
                throw new InconsistentStateException(
                    $"Version conflict (WriteState): ServiceId={_clusterOptions.ServiceId} " +
                    $"ProviderName={_storageName} GrainType={nameof(T)} " +
                    $"GrainReference={grainId.GetGuidKey()}.");
            }

            grainState.ETag = null;
            grainState.State = Activator.CreateInstance<T>();
            this._options.RootDirectory.Delete(filename);

            return Task.CompletedTask;
        }

        public void Participate(ISiloLifecycle observer)
        {
            observer.Subscribe(
                OptionFormattingUtilities.Name<FileGrainStorage>(_storageName),
                ServiceLifecycleStage.ApplicationServices,
                Init);
        }
    }
}
