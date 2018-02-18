using McMaster.Extensions.CommandLineUtils;

namespace Itron.Tools.CosmoDb.Cli.Commands
{
    [Subcommand("new-db", typeof(NewDbCommand))]
    public class RootCommand
    {
    }
}