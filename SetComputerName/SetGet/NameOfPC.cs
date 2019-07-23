using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security;

namespace SetComputerName
{
    class NameOfPC
    {  
        public static string SetMachineName()
        {           
            // Set up machine name
            string machineName = Environment.MachineName;
            Console.Write("Type computer name: ");
            string newName = Console.ReadLine();
            Console.Clear();               
            String RegLocComputerName = @"SYSTEM\CurrentControlSet\Control\ComputerName\ComputerName";
            try
            {
                //Invokes a method on the WMI object.
                string compPath = "Win32_ComputerSystem.Name='" + System.Environment.MachineName + "'";
                using (ManagementObject mo = new ManagementObject(new ManagementPath(compPath)))
                {
                    ManagementBaseObject inputArgs = mo.GetMethodParameters("Rename");
                    inputArgs["Name"] = newName;
                    ManagementBaseObject output = mo.InvokeMethod("Rename", inputArgs, null);
                    uint retValue = (uint)Convert.ChangeType(output.Properties["ReturnValue"].Value, typeof(uint));
                    if (retValue != 0)
                    {
                        throw new Exception("Computer could not be changed due to unknown reason.");
                    }
                }

                RegistryKey ComputerName = Registry.LocalMachine.OpenSubKey(RegLocComputerName);
                if (ComputerName == null)
                {
                    throw new Exception("Registry location '" + RegLocComputerName + "' is not readable.");
                }
                if (((String)ComputerName.GetValue("ComputerName")) != newName)
                {
                    throw new Exception("The computer name was set by WMI but was not updated in the registry location: '" + RegLocComputerName + "'");
                }
                ComputerName.Close();
                ComputerName.Dispose();
            }
            catch (ArgumentNullException)
            {
                return "Type a name of computer, don't use spec. characters";
            }
            catch (ObjectDisposedException)
            {
                return "Closed keys cannot be accessed";
            }
            catch (SecurityException)
            {
                return "You not have the permissions, start app as Administrator";
            }
            catch (OverflowException)
            {
                return "Number is out of the range of conversionType";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                if (newName != string.Empty)
                    Console.WriteLine("New expected name after restart: " + newName); 
                else
                    Console.WriteLine(System.Environment.MachineName); 
            }
            return System.Environment.MachineName;    
            // return System.Environment.GetEnvironmentVariable("COMPUTERNAME"); 
            // return System.Net.Dns.GetHostName();
        }
    }
}
