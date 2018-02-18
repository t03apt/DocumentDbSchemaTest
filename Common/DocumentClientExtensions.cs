using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Experimental.Tools.CosmoDb.Common
{
    public static class DocumentClientExtensions
    {
        public static async Task<ResourceResponse<DocumentCollection>> CreateDocumentCollectionIfNotExistsAsync(this DocumentClient client, Database database, DocumentCollection collection)
        {
            if (database == null) throw new ArgumentException(nameof(database));
            if (collection == null) throw new ArgumentException(nameof(collection));

            return await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(database.Id), collection);
        }
    }
}
