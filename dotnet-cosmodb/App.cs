using Itron.Tools.CosmoDb.Cli.Commands;
using McMaster.Extensions.CommandLineUtils;

namespace Itron.Tools.CosmoDb.Cli
{
    [Command, Subcommand("new-db", typeof(NewDbCommand))]
    public class App : ICommand
    {
        public int OnExecute()
        {
            return ExitCodes.Ok;
        }
    }
}