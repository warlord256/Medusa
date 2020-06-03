using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Medusa
{
    public class Users
    {
        [JsonProperty(PropertyName="userId")]
        public string userId { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string password { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
    }
}
