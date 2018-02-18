using System;
using Experimental.Tools.CosmoDb.Common;
using Microsoft.Azure.Documents.Client;

namespace Experimental.Tools.CosmoDb.Cli.Services
{
    internal class DbStoreFactory : IDbStoreFactory
    {
        public DbStore Create(string url, string authKey) => Create(new Uri(url), authKey);

        public DbStore Create(Uri url, string authKey) => new DbStore(new DocumentClient(url, authKey));
    }
}
