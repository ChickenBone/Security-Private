using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Voyager.Helpers
{

    // TODO: Finish

    class Update
    {
        public static void start()
        {
            try
            {
                Debug.WriteLine("Updating");
                string IP = Entry.IP;
                int port = Entry.Port;
                WebClient Client = new WebClient();
                Client.DownloadFile(String.Format("{0}:{1}/update/Voyager.exe", IP, port.ToString()), System.Reflection.Assembly.GetExecutingAssembly().Location.ToString() + @"\update\Voyager.exe");
                Debug.WriteLine("Update Complete");
            }
            catch
            {
                Error update = new Error(1);
            }
        }
    }
}
