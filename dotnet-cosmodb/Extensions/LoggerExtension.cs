using System;
using Serilog;

namespace Experimental.Tools.CosmoDb.Cli.Extensions
{
    public static class LoggerExtension
    {
        private const string GenericErrorMessage = "Error";

        public static void LogException(this ILogger logger, Exception ex) 
            => logger.Error(ex, GenericErrorMessage);

        public static void Fatal(this ILogger logger, Exception ex)
            => logger.Error(ex, GenericErrorMessage);
    }
}
