using Itron.Tools.CosmoDb.Cli.Commands;
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
            return CommandLineApplication.Execute<Startup>(args);
        }

        public int OnExecute()
        {
            var levelSwitch = new LoggingLevelSwitch(LogLevel);
            var logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.Console(standardErrorFromLevel: LogEventLevel.Verbose).CreateLogger();

            Container.Build(logger);

            return CommandLineApplication.Execute<App>(_args);
        }
    }
}
