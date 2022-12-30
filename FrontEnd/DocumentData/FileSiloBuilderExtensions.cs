using Orleans.Runtime;
using Orleans.Storage;

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
}