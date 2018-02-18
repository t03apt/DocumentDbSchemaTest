using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace Itron.Tools.CosmoDb.Cli.Commands.Databases
{
    internal abstract class CosmoDbBaseCommand
    {
        [Required]
        [Option(LongName = "url", Description = "CosmoDB endpoint")]
        public string Url { get; set; }

        [Required]
        [Option(LongName = "authKey", Description = "CosmoDB authentication key")]
        public string AuthKey { get; set; }
    }
}
