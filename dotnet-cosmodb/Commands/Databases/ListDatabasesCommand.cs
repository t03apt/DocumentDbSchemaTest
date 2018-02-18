using System;
using System.Threading.Tasks;
using Itron.Tools.CosmoDb.Cli.Services;
using McMaster.Extensions.CommandLineUtils;

namespace Itron.Tools.CosmoDb.Cli.Commands.Databases
{
    [Command]
    internal class ListDatabasesCommand : CosmoDbBaseCommand, ICommand
    {
        private readonly IDbStoreFactory _dbStoreFactory;

        public ListDatabasesCommand() : this(
            Container.GetService<IDbStoreFactory>())
        { }

        public ListDatabasesCommand(IDbStoreFactory dbStoreFactory)
        {
            _dbStoreFactory = dbStoreFactory ?? throw new ArgumentNullException(nameof(dbStoreFactory));
        }

        public async Task<int> OnExecute(CommandLineApplication app, IConsole console)
        {
            using (var dbStore = _dbStoreFactory.Create(Url, AuthKey))
            {
                var databases = await dbStore.GetDatabases();
                foreach (var database in databases)
                {
                    console.WriteLine(database.Id);
                }
            }

            return ExitCodes.Ok;
        }

    }
}
