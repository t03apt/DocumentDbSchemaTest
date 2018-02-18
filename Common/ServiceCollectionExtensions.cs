using Microsoft.Extensions.DependencyInjection;

namespace Common
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
