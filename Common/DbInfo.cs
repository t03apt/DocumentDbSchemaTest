using System.Collections.Generic;
using Microsoft.Azure.Documents;

namespace Common
{
    public class DbInfo
    {
        public string Id { get; set; }

        public IList<DocumentCollection> Collections { get; set; }
    }
}
