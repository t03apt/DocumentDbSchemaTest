using Experimental.Tools.CosmoDb.Common;

namespace Experimental.Tools.CosmoDb.Cli.Services
{
    internal interface IDbStoreFactory
    {
        DbStore Create(string url, string authKey);
    }
}