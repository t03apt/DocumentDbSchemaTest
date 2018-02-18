using Microsoft.Extensions.DependencyInjection;

namespace Experimental.Tools.CosmoDb.Common
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection RegisterCommonServices(this ServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<DbInfoParser>()
                .AddSingleton<FileReader>();

            return serviceCollection;
        }
    }
}
