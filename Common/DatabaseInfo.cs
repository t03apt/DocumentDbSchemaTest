using System.Collections.Generic;
using Microsoft.Azure.Documents;

namespace Common
{
    public class DatabaseInfo
    {
        public string Id { get; set; }

        public IList<DocumentCollection> Collections { get; set; }
    }
}
