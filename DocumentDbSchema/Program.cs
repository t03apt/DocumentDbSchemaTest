using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace DocumentDbSchema
{
    class Program
    {
        private static string CosmoDbEndpoint => GetConfigValue();
        private static string AuthKey => GetConfigValue();
        private static string DatabaseId => GetConfigValue();

        private static string GetConfigValue([CallerMemberName] string settingsKey = null) => Configuration[settingsKey];

        private static IConfigurationRoot Configuration { get; set; }

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DocumentCollectionContractResolver()
        };

        private static void Main()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            var generator = new JSchemaGenerator
            {
                ContractResolver = new DocumentCollectionContractResolver()
            };

            generator.GenerationProviders.Add(new StringEnumGenerationProvider());

            JSchema schema = generator.Generate(typeof(DocumentCollection));

            Console.WriteLine(schema.ToString());

            File.WriteAllText(@"C:\temp\collection-schema.json", schema.ToString());

            CreateCollection().Wait();

            Console.WriteLine("OK");
        }

        private static async Task<DocumentCollection> CreateCollection()
        {
            var sampleCollection = GetSampleCollection();
            var client = new DocumentClient(new Uri(CosmoDbEndpoint), AuthKey);

            var database = new Database { Id = DatabaseId };

            database = await client.CreateDatabaseIfNotExistsAsync(database);
            return await client.CreateDocumentCollectionIfNotExistsAsync(database, sampleCollection);
        }

        private static DocumentCollection GetSampleCollection()
        {
            string GetSampleCollectionJson()
            {
                var assembly = typeof(Program).Assembly;
                var resourceName = $"{typeof(Program).Namespace}.sample-collection.json";

                string data;

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                using (var reader = new StreamReader(stream))
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

                    var values = props.Select(o => o.Value).ToList();

                    var innerProps = values.OfType<JObject>()
                        .Union(values.OfType<JArray>().SelectMany(o => o.Children()).OfType<JObject>())
                        .ToList();

                    innerProps.ForEach(SortProperties);
                }
            }

            var sample = GetSampleCollectionJson();
            var collection = JsonConvert.DeserializeObject<DocumentCollection>(sample, SerializerSettings);
            return collection;
        }
    }
}
