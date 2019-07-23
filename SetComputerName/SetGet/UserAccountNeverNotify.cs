using System;
using Microsoft.Win32;
using System.Security;

namespace SetComputerName
{
    //Setup policies of the OS
    class UserAccountNeverNotify
    {
        public static string UserAccount()
        {
            try
            {
                RegistryKey key =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true);
                key.SetValue("EnableLUA", "0");
                key.Close();
            }
            catch (ArgumentNullException)
            {
                return "Type a name.";
            }
            catch (ObjectDisposedException e)
            {
                return e.Message;
            }
            catch (SecurityException e)
            {
                return e.Message;
            }
            catch(Exception e)
            {
                return e.Message;
            }
            return "OK";
        }
    }
}
