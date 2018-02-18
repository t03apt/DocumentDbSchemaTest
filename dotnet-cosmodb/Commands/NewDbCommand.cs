using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Common;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Azure.Documents.Client;

namespace Itron.Tools.CosmoDb.Cli.Commands
{
    [Command]
    internal class NewDbCommand: IAsyncCommand
    {
        private readonly DbInfoParser _dbInfoParser;
        private readonly FileReader _fileReader;

        [Required]
        [Option(LongName = "file", Description = "Data file")]
        public string FilePath { get; set; }

        [Required]
        [Option(LongName = "url", Description = "CosmoDB endpoint")]
        public string Url { get; set; }

        [Required]
        [Option(LongName = "authKey", Description = "CosmoDB authentication key")]
        public string AuthKey { get; set; }

        public NewDbCommand() : this (
            Container.GetService<DbInfoParser>(),
            Container.GetService<FileReader>())
        { }

        public NewDbCommand(
            DbInfoParser dbInfoParser,
            FileReader fileReader)
        {
            _dbInfoParser = dbInfoParser ?? throw new ArgumentException(nameof(dbInfoParser));
            _fileReader = fileReader ?? throw new ArgumentException(nameof(fileReader));
        }

        public async Task<int> OnExecute()
        {
            var json = _fileReader.ReadAll(FilePath);
            var dbInfo = _dbInfoParser.Parse(json);

            using (var dbStore = new DbStore(new DocumentClient(new Uri(Url), AuthKey)))
            {
                await dbStore.CreateDatabaseAsync(dbInfo);
            }

            return ExitCodes.Ok;
        }
    }
}
