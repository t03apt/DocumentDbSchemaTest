using System;
using System.IO;
using System.Linq;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace DocumentDbSchema
{
    class Program
    {
        static void Main(string[] args)
        {
            JSchemaGenerator generator = new JSchemaGenerator
            {
                ContractResolver = new DocumentCollectionContractResolver(),
            };

            generator.GenerationProviders.Add(new StringEnumGenerationProvider());

            JSchema schema = generator.Generate(typeof(DocumentCollection));

            Console.WriteLine(schema.ToString());

            //File.WriteAllText(@"C:\temp\collection-schema.json", schema.ToString());

            //TestSchema();
        }

        private static void TestSchema()
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DocumentCollectionContractResolver()
            };

            var sample = GetSampleCollectionJson();
            var collection = JsonConvert.DeserializeObject<DocumentCollection>(sample, serializerSettings);

            var collectionAsJson = JsonConvert.SerializeObject(collection, serializerSettings);

            //File.WriteAllText(@"C:\temp\sample.txt", $"{collectionAsJson}{Environment.NewLine}{sample}");

            Console.WriteLine(collectionAsJson == sample);

            string GetSampleCollectionJson()
            {
                var assembly = typeof(Program).Assembly;
                var resourceName = $"{typeof(Program).Namespace}.sample-collection.json";

                string data;

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    data = reader.ReadToEnd();
                }

                var json = JObject.Parse(data);
                SortProperties(json);

                return json.ToString(Formatting.None);

                void SortProperties(JObject jObj)
                {
                    var props = jObj.Properties().ToList();

                    props.ForEach(o => o.Remove());

                    props.OrderBy(p => p.Name).ToList().ForEach(jObj.Add);

                    var values = props.Select(o => o.Value);

                    var innerProps = values.OfType<JObject>()
                        .Union(values.OfType<JArray>().SelectMany(o => o.Children()).OfType<JObject>())
                        .ToList();

                    innerProps.ForEach(SortProperties);
                }
            }
        }
    }
}
