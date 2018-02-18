using Common;

namespace Itron.Tools.CosmoDb.Cli.Services
{
    internal interface IDbStoreFactory
    {
        DbStore Create(string url, string authKey);
    }
}