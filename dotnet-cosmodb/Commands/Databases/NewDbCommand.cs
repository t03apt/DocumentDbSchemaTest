using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Experimental.Tools.CosmoDb.Cli.Services;
using Experimental.Tools.CosmoDb.Common;
using McMaster.Extensions.CommandLineUtils;

namespace Experimental.Tools.CosmoDb.Cli.Commands.Databases
{
    [Command]
    internal class NewDbCommand : CosmoDbBaseCommand, ICommand
    {
        private readonly DbInfoParser _dbInfoParser;
        private readonly FileReader _fileReader;
        private readonly IDbStoreFactory _dbStoreFactory;

        [Required]
        [Option(LongName = "file", Description = "Data file")]
        public string FilePath { get; set; }

        public NewDbCommand() : this(
            Container.GetService<DbInfoParser>(),
            Container.GetService<FileReader>(),
            Container.GetService<IDbStoreFactory>())
        { }

        public NewDbCommand(
            DbInfoParser dbInfoParser,
            FileReader fileReader,
            IDbStoreFactory dbStoreFactory)
        {
            _dbInfoParser = dbInfoParser ?? throw new ArgumentException(nameof(dbInfoParser));
            _fileReader = fileReader ?? throw new ArgumentException(nameof(fileReader));
            _dbStoreFactory = dbStoreFactory ?? throw new ArgumentNullException(nameof(dbStoreFactory));
        }

        public async Task<int> OnExecute(CommandLineApplication app, IConsole console)
        {
            var json = _fileReader.ReadAll(FilePath);
            var dbInfo = _dbInfoParser.Parse(json);

            using (var dbStore = _dbStoreFactory.Create(Url, AuthKey))
            {
                await dbStore.CreateDatabaseAsync(dbInfo);
            }

            return ExitCodes.Ok;
        }
    }
}
