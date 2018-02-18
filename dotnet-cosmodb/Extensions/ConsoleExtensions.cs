using McMaster.Extensions.CommandLineUtils;

namespace Experimental.Tools.CosmoDb.Cli.Extensions
{
    public static class ConsoleExtensions
    {
        public static void MustSpecifySubcommand(this IConsole console)
            => console.WriteLine("You must specify a subcommand.");
    }
}