using Experimental.Tools.CosmoDb.Cli.Services;
using Experimental.Tools.CosmoDb.Common;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Experimental.Tools.CosmoDb.Cli
{
    internal sealed class Container
    {
        private static ServiceProvider _provider;

        public static T GetService<T>() => _provider.GetRequiredService<T>();

        public static void Build(ILogger logger)
        {
            var serviceCollection = new ServiceCollection()
                .RegisterCommonServices()
                .AddSingleton<IDbStoreFactory, DbStoreFactory>()
                .AddScoped(_ => logger);

            _provider = serviceCollection.BuildServiceProvider();
        }
    }
}
