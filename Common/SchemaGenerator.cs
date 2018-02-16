using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Common
{
    public class SchemaGenerator
    {
        public JSchema GenerateSchema()
        {
            var generator = new JSchemaGenerator
            {
                ContractResolver = new DocumentCollectionContractResolver()
            };

            generator.GenerationProviders.Add(new StringEnumGenerationProvider());

            return generator.Generate(typeof(DatabaseInfo));
        }
    }
}
