using System.Collections.Generic;
using Microsoft.Azure.Documents;

namespace Experimental.Tools.CosmoDb.Common
{
    public class DbInfo
    {
        public string Id { get; set; }

        public IList<DocumentCollection> Collections { get; set; }
    }
}
