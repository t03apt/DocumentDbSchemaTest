using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace Experimental.Tools.CosmoDb.Cli
{
    internal interface ICommand
    {
        Task<int> OnExecute(CommandLineApplication app, IConsole console);
    }
}
