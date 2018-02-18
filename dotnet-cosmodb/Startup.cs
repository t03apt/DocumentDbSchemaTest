using System;
using System.Threading.Tasks;
using Itron.Tools.CosmoDb.Cli.Commands;
using Itron.Tools.CosmoDb.Cli.Extensions;
using McMaster.Extensions.CommandLineUtils;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Itron.Tools.CosmoDb.Cli
{
    [Command(ThrowOnUnexpectedArgument = false), HelpOption]
    internal class Startup : ICommand
    {
        [Option(ShortName = "ll", LongName = "loglevel", Description = "The image for the new container")]
        public LogEventLevel LogLevel { get; set; } = LogEventLevel.Warning;

        private static string[] _args;

        public static int Main(string[] args)
        {
            _args = args;
            return CommandLineApplication.ExecuteAsync<Startup>(args).Result;
        }

        public async Task<int> OnExecute(CommandLineApplication app, IConsole console)
        {
            var levelSwitch = new LoggingLevelSwitch(LogLevel);
            var logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.Console(standardErrorFromLevel: LogEventLevel.Verbose).CreateLogger();

            try
            {
                Container.Build(logger);
                return await CommandLineApplication.ExecuteAsync<RootCommand>(_args);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                return ExitCodes.Error;
            }
        }
    }
}
