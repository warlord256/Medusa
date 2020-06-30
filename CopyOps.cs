using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Medusa
{
    class CopyOps
    {
        public static async Task copyWorkItem(WorkItemMessage message)
        {
            makeHeader(message);
        }

        private static async void makeHeader(WorkItemMessage message)
        {
            string result = JsonConvert.SerializeObject(message);
            Console.WriteLine(result);          
        }
      
        private static async void breakHeader(string header)
        {
            WorkItemMessage message = JsonConvert.DeserializeObject<WorkItemMessage>(header);
            foreach(var itr in message.content)
            {
                Clipboard.SetText(itr);
            }
            //Clipboard.SetText(message.content);
        }
    }

    class WorkItemMessage
    {
        public string userId { get; set; }
        public string fileType { get;set; }
        public string[] content { get; set; }
    }
}
