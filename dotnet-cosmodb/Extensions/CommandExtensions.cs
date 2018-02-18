using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace Itron.Tools.CosmoDb.Cli.Extensions
{
    internal static class CommandExtensions
    {
        public static Task<int> MustSpecifySubcommand(this ICommand command, CommandLineApplication app, IConsole console)
        {
            console.MustSpecifySubcommand();
            app.ShowHelp();
            return Task.FromResult(ExitCodes.Error);
        }
    }
}
