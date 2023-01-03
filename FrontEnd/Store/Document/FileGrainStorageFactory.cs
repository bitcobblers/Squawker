using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orleans.Configuration.Overrides;
using Orleans.Storage;

namespace FrontEnd.Store.DocumentData
{
    public static class FileGrainStorageFactory
    {
        internal static IGrainStorage Create(
            IServiceProvider services, string name)
        {
            using var scope = services.CreateScope();
            IOptionsSnapshot<FileGrainStorageOptions> optionsSnapshot
                = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<FileGrainStorageOptions>>();

            return ActivatorUtilities.CreateInstance<FileGrainStorage>(
                services,
                name,
                optionsSnapshot.Get(name),
                services.GetProviderClusterOptions(name));
        }
    }
}
