using System;
using Serilog;

namespace Itron.Tools.CosmoDb.Cli.Extensions
{
    public static class LoggerExtension
    {
        public static void LogException(this ILogger logger, Exception ex)
        {
            logger.Error(ex, "Error");
        }
    }
}
