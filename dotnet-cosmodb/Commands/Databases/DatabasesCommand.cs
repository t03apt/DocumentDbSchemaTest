using System.Threading.Tasks;
using Experimental.Tools.CosmoDb.Cli.Extensions;
using McMaster.Extensions.CommandLineUtils;

namespace Experimental.Tools.CosmoDb.Cli.Commands.Databases
{
    [Command, HelpOption]
    [Subcommand("new", typeof(NewDbCommand))]
    [Subcommand("list", typeof(ListDatabasesCommand))]
    [Subcommand("export-json-schema", typeof(ExportJsonSchemaCommand))]
    internal class DatabasesCommand : ICommand
    {
        public Task<int> OnExecute(CommandLineApplication app, IConsole console)
        {
            return this.MustSpecifySubcommand(app, console);
        }
    }
}
