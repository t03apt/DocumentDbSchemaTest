using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Experimental.Tools.CosmoDb.Common;
using McMaster.Extensions.CommandLineUtils;

namespace Experimental.Tools.CosmoDb.Cli.Commands.Databases
{
    [Command]
    internal class ExportJsonSchemaCommand : ICommand
    {
        private readonly FileWriter _fileWriter;
        private readonly SchemaGenerator _schemaGenerator;

        public ExportJsonSchemaCommand()
            : this(
            Container.GetService<FileWriter>(),
            Container.GetService<SchemaGenerator>())
        {
        }

        public ExportJsonSchemaCommand(FileWriter fileWriter, SchemaGenerator schemaGenerator)
        {
            _fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
            _schemaGenerator = schemaGenerator ?? throw new ArgumentNullException(nameof(schemaGenerator));
        }

        [Required]
        [Option(LongName = "file", Description = "Target file path")]
        public string FilePath { get; set; }

        public Task<int> OnExecute(CommandLineApplication app, IConsole console)
        {
            var schema = _schemaGenerator.GenerateSchema();
            _fileWriter.SaveToFile(FilePath, schema.ToString());
            return Task.FromResult(ExitCodes.Ok);
        }
    }
}
