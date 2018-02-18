using System.Threading.Tasks;
using Itron.Tools.CosmoDb.Cli.Extensions;
using McMaster.Extensions.CommandLineUtils;

namespace Itron.Tools.CosmoDb.Cli.Commands.Databases
{
    [Command, HelpOption]
    [Subcommand("new", typeof(NewDbCommand))]
    [Subcommand("list", typeof(ListDatabasesCommand))]
    internal class DatabasesCommand : ICommand
    {
        public Task<int> OnExecute(CommandLineApplication app, IConsole console)
        {
            return this.MustSpecifySubcommand(app, console);
        }
    }
}
