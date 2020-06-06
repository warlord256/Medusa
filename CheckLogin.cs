using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.Cosmos;

namespace Medusa
{
    class CheckLogin
    {
        public  static async Task<Boolean> Operations(string userName,string passWord)
        {
            return await confirmLogin(userName, passWord);
        }

        public static async Task Registrations(string userName,string passWord)
        {
             await Register(userName, passWord);
        }

        private static async Task<Boolean> confirmLogin(string userName, string passWord)
        {
            var container = Shared.Client.GetContainer("Families", "Families");
            var sql = "SELECT * FROM c ";
            var iterator = container.GetItemQueryIterator<dynamic>(sql);
            var pages = await iterator.ReadNextAsync();

            Console.WriteLine();
            Console.WriteLine("Checking login credentials...");

            foreach(var itr in pages)
            {
                if (itr.userId == userName && itr.password == passWord)
                {
                    MessageBox.Show("Login Successful", "Congratulations!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            MessageBox.Show("Incorrect Credentials", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        private static async Task Register(string userName,string password)
        {
            Console.WriteLine();
            Console.WriteLine(">>> Registering Users <<<");

            var container = Shared.Client.GetContainer("Families", "Families");

            dynamic registerdynamic = new
            {
                id = Guid.NewGuid(),
                username = userName,
                password = password,
                address = new
                {
                    zipCode = "12344"
                }
            };
          
            await container.CreateItemAsync(registerdynamic, new PartitionKey("12344"));
            Console.WriteLine($"Created New User with ID {registerdynamic.id}");
           // object p = await container.CreateItemAsync<dynamic>();
        }


    }
}
