using System.Threading.Tasks;

namespace Itron.Tools.CosmoDb.Cli.Commands
{
    internal interface IAsyncCommand
    {
        Task<int> OnExecute();
    }
}