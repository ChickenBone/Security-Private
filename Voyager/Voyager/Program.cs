using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Voyager
{
    class Program
    {
        public static TCPClient client = new TCPClient();
        public static string encryptionkey = "boysean";
        
        static void Main(string[] args)
        {
            Debug.WriteLine("Starting Voyager");
            Entry.init();
        }
        public static void send(string message)
        {
            if (client.connected)
            {
                client.SendMessage(message);
            }
        }
    }
}
