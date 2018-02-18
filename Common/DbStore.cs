using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Experimental.Tools.CosmoDb.Common
{
    public class DbStore : IDisposable
    {
        private readonly DocumentClient _client;

        public DbStore(DocumentClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task CreateDatabaseAsync(DbInfo databaseInfo)
        {
            var database = new Database { Id = databaseInfo.Id };

            database = await _client.CreateDatabaseIfNotExistsAsync(database);

            foreach (var collection in databaseInfo.Collections)
            {
                await _client.CreateDocumentCollectionIfNotExistsAsync(database, collection);
            }
        }

        public async Task<IList<Database>> GetDatabases()
        {
            var feed = await _client.ReadDatabaseFeedAsync();
            return feed.ToList();
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
