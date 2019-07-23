using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SetComputerName
{
    class ChangeBootTimeout
    {       

        public static string ChangeBoot(string arg)
        {     
            //Provides access to local processes and enables you to start and
            //stop local system processes.
            Process cmd = new Process();            
            try
            {
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Verb = "runas";
            cmd.StartInfo.Arguments = "";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.RedirectStandardError = true;
            
            cmd.Start();
            // OR , "netsh firewall set opmode disable"
            cmd.StandardInput.WriteLine(arg);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            //Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            //System.Diagnostics.Process.Start("CMD.exe", "/C bcdedit /timeout 0");        
            }
            catch (ArgumentNullException)
            {
                return "Problem with adjusting CMD.";
            }
            catch (InvalidOperationException)
            {
                return "Probleblem with input setup.";
            }
            return "OK";
        }
    }
}
