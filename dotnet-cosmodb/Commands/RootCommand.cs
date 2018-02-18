using Itron.Tools.CosmoDb.Cli.Commands.Databases;
using McMaster.Extensions.CommandLineUtils;

namespace Itron.Tools.CosmoDb.Cli.Commands
{
    [Command]
    [Subcommand("databases", typeof(DatabasesCommand))]
    public class RootCommand
    {
    }
}