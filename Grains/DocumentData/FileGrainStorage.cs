using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Orleans.Configuration;
using Orleans.Configuration.Overrides;
using Orleans.Runtime;
using Orleans.Storage;
using System.Text.Json;

namespace Grains.DocumentData
{

    public static class FileSiloBuilderExtensions
    {
        public static ISiloBuilder AddFileGrainStorage(
            this ISiloBuilder builder,
            string providerName,
            Action<FileGrainStorageOptions> options)
        {
            return builder.ConfigureServices(
                services => services.AddFileGrainStorage(providerName, options));
        }

        public static IServiceCollection AddFileGrainStorage(
            this IServiceCollection services,
            string providerName,
            Action<FileGrainStorageOptions> options)
        {
            services.AddOptions<FileGrainStorageOptions>(providerName).Configure(options);

            return services.AddSingletonNamedService(providerName, FileGrainStorageFactory.Create)
                .AddSingletonNamedService(
                    providerName,
                    (s, n) => (ILifecycleParticipant<ISiloLifecycle>)s.GetRequiredServiceByName<IGrainStorage>(n));
        }
    }

    public static class FileGrainStorageFactory
    {
        internal static IGrainStorage Create(
            IServiceProvider services, string name)
        {
            IOptionsSnapshot<FileGrainStorageOptions> optionsSnapshot =
                services.GetRequiredService<IOptionsSnapshot<FileGrainStorageOptions>>();

            return ActivatorUtilities.CreateInstance<FileGrainStorage>(
                services,
                name,
                optionsSnapshot.Get(name),
                services.GetProviderClusterOptions(name));
        }
    }

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

            grainState.State = fileInfo?.Exists ?? false
                ? JsonSerializer.Deserialize<T>(fileInfo.CreateReadStream())
                : Activator.CreateInstance<T>();

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
