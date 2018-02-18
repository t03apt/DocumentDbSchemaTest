using System;
using Common;
using Microsoft.Azure.Documents.Client;

namespace Itron.Tools.CosmoDb.Cli.Services
{
    internal class DbStoreFactory : IDbStoreFactory
    {
        public DbStore Create(string url, string authKey) => Create(new Uri(url), authKey);

        public DbStore Create(Uri url, string authKey) => new DbStore(new DocumentClient(url, authKey));
    }
}
