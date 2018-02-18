using System;
using System.IO;
using System.Runtime.CompilerServices;
using Common;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Schema;

namespace DocumentDbTools
{
    class Program
    {
        private static string CosmoDbEndpoint => GetConfigValue();
        private static string AuthKey => GetConfigValue();

        private static string GetConfigValue([CallerMemberName] string settingsKey = null) => Configuration[settingsKey];

        private static IConfigurationRoot Configuration { get; set; }

        private static void Main()
        {
            // Call: dotnet user-secrets set AuthKey YOUR_AUTH_KEY

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            var dbInfoParser = new DbInfoParser();
            var db = dbInfoParser.Parse(GetSample());
            var dbStore = new DbStore(new DocumentClient(new Uri(CosmoDbEndpoint), AuthKey));
            dbStore.CreateDatabaseAsync(db).Wait();

            Console.WriteLine("OK");
        }

        private static JSchema GetSchema()
        {
            var generator = new SchemaGenerator();
            var schema =  generator.GenerateSchema();

            //File.WriteAllText(@"C:\temp\collection-schema.json", schema.ToString());

            return schema;
        }

        private static string GetSample()
        {
            var assembly = typeof(Program).Assembly;
            var resourceName = $"{typeof(Program).Namespace}.sample-database.json";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
