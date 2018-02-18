using System.Threading.Tasks;
using Experimental.Tools.CosmoDb.Cli.Commands.Databases;
using Experimental.Tools.CosmoDb.Cli.Extensions;
using McMaster.Extensions.CommandLineUtils;

namespace Experimental.Tools.CosmoDb.Cli.Commands
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
