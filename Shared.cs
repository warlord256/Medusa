using System;
using System.Collections.Generic;

using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace Medusa
{
    public static class Shared
    {
        public static CosmosClient Client { get; private set; }

        static Shared()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();      

            //var endpoint = config["https://medusa.documents.azure.com:443/"];
        
            Client = new CosmosClient(endpoint, masterKey);
        }
        public static string EndPoint()
        {
            return endpoint;
        }
        public static string MasterKey()
        {
            return masterKey;
        }
        public static string PrimaryKey()
        {
            return primaryKey;
        }
        private static readonly string endpoint = "https://medusadb.documents.azure.com:443/";
        private static readonly string masterKey = "lqKXNQZZn4DaUPwptMG58iLWYovnbganfD1GlzhgwAejp39vydxsOTIVIGrQ6kGdEDehpVMp6RFzK4Ep7OBERg==";
        private static readonly string primaryKey = "B5f2e7jx6i6GeMT2tUP5fdXXsn2sDBXZY8pK5QAnqSocm9HmvSgEs6du9PMOP0CZEcHphn9ZEnpqtrASWzp3Xg==";
    }
}
