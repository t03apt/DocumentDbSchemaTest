using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace Experimental.Tools.CosmoDb.Cli.Extensions
{
    internal static class CommandExtensions
    {
        public static Task<int> MustSpecifySubcommand(this ICommand command, CommandLineApplication app, IConsole console)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            console.MustSpecifySubcommand();
            app.ShowHelp();
            return Task.FromResult(ExitCodes.Error);
        }
    }
}
