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
            var endpoint = "https://medusadb.documents.azure.com:443/";
            var masterKey = "lqKXNQZZn4DaUPwptMG58iLWYovnbganfD1GlzhgwAejp39vydxsOTIVIGrQ6kGdEDehpVMp6RFzK4Ep7OBERg==";

            Client = new CosmosClient(endpoint, masterKey);
        }

    }
}
