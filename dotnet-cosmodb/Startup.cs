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
        [Option(ShortName = "ll", LongName = "loglevel", Description = "Log level. Default:Warning. Possible values:" + LogLevelValues)]
        public string LogLevel { get; set; } = LogEventLevel.Warning.ToString();

        private static string[] _args;

        private const string LogLevelValues = 
            nameof(LogEventLevel.Verbose) + "|" +
            nameof(LogEventLevel.Debug) + "|" +
            nameof(LogEventLevel.Warning) + "|" +
            nameof(LogEventLevel.Information) + "|" +
            nameof(LogEventLevel.Error) + "|" +
            nameof(LogEventLevel.Fatal);

        public static int Main(string[] args)
        {
            _args = args;
            return CommandLineApplication.ExecuteAsync<Startup>(args).Result;
        }

        public async Task<int> OnExecute(CommandLineApplication app, IConsole console)
        {
            if (!Enum.TryParse<LogEventLevel>(LogLevel, out var logLevel))
            {
                throw new ArgumentOutOfRangeException(nameof(LogLevel), LogLevel, null);
            }

            var levelSwitch = new LoggingLevelSwitch(logLevel);
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
