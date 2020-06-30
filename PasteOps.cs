
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System; // Namespace for Console output
using System.Configuration; // Namespace for ConfigurationManager
using System.Threading.Tasks; // Namespace for Task
using Azure.Storage.Queues; // Namespace for Queue storage types
using Azure.Storage.Queues.Models; // Namespace for PeekedMessage
using System.Windows.Forms;

namespace Medusa
{
    class PasteOps
    {

        public static async Task ReceiveWorkItem(string header)
        {
             ReceivePaste(header);
        }

        private static  async void ReceivePaste(string header)
        {
           
        }
    }
}
