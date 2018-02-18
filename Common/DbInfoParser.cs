using Newtonsoft.Json;

namespace Common
{
    public class DbInfoParser
    {
        public DbInfo Parse(string json)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DocumentCollectionContractResolver()
            };

            return JsonConvert.DeserializeObject<DbInfo>(json, serializerSettings);
        }
    }
}
