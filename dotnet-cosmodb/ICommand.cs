using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace Itron.Tools.CosmoDb.Cli
{
    internal interface ICommand
    {
        Task<int> OnExecute(CommandLineApplication app, IConsole console);
    }
}