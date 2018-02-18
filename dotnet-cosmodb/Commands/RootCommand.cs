using System.Threading.Tasks;
using Itron.Tools.CosmoDb.Cli.Commands.Databases;
using Itron.Tools.CosmoDb.Cli.Extensions;
using McMaster.Extensions.CommandLineUtils;

namespace Itron.Tools.CosmoDb.Cli.Commands
{
    [Command]
    [Subcommand("databases", typeof(DatabasesCommand))]
    public class RootCommand : ICommand
    {
        public Task<int> OnExecute(CommandLineApplication app, IConsole console)
        {
            return this.MustSpecifySubcommand(app, console);
        }
    }
}