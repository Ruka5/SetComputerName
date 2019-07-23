using System;
using System.Globalization;
using System.Threading;
using System.Configuration;

namespace SetComputerName
{
    class UserMenuInfo
    {     
        // Conditioning on ConfigFile   
        private static string ConfigurationManagerProcess(string cmpName, string cmpData)
        {
            if(ConfigurationManager.AppSettings[cmpName] == null)
                return cmpData;
            else 
                return ConfigurationManager.AppSettings[cmpName];
            //ConfigurationManager.AppSettings[cmpName] == null ? cmpData : ConfigurationManager.AppSettings[cmpName];
        }

        //Static adjustment
        private static string iPAddress = null;
        private static string mask = ConfigurationManagerProcess("mask", "255.255.255.0");
        private static string gateway = null;
        private static string prefferedDNS = ConfigurationManagerProcess("prefferedDNS", "10.122.212.11");
        private static string alternateDNS = ConfigurationManagerProcess("alternateDNS", "10.122.212.12");
        private static string changeBoot = ConfigurationManagerProcess("changeBoot", "10");
        private static string nameOfGroup = ConfigurationManagerProcess("nameOfGroup", "MODULE");

        public static void TCP_IP_Do()
        {
            Console.Write("iPAddress: ");
            iPAddress = Console.ReadLine();
            Console.Write("Gateway: ");
            gateway = Console.ReadLine();
            NetworkManagement.setIP(iPAddress, mask, gateway, new[] { prefferedDNS, alternateDNS });
        }   
        public static void Write()
        {
            TCP_IP_Do();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(@"Don't use special characters \|!#$%&/()=?»«@£§€{}.-;'<>_,");
            Console.ResetColor();
            Console.WriteLine(" 1. Computer Name            - " + NameOfPC.SetMachineName());
            Console.WriteLine(" 2. WorkGroup Name           - " + SetUpWorkGroup.SetGroupName(nameOfGroup));
            Console.WriteLine(" 3. Firewall Set Off         - " + ChangeBootTimeout.ChangeBoot("netsh advfirewall set allprofiles state off"));
            Console.WriteLine(" 4. User Never Notify        - " + UserAccountNeverNotify.UserAccount());
            Console.WriteLine(" 5. Power Button off         - " + ChangeBootTimeout.ChangeBoot("powercfg -setacvalueindex SCHEME_CURRENT 4f971e89-eebd-4455-a8de-9e59040e7347 7648efa3-dd9c-4e3e-b566-50f929386280 0"));
            Console.WriteLine(" 6. Turn off sleep           - " + ChangeBootTimeout.ChangeBoot("powercfg -setacvalueindex SCHEME_CURRENT 4f971e89-eebd-4455-a8de-9e59040e7347 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c 0"));
            Console.WriteLine(" 7. Get IP                   - " + NetworkManagement.ListIP(1));
            Console.WriteLine(" 8. Get MASK                 - " + NetworkManagement.ListIP(2));
            Console.WriteLine(" 9. Get Gateway              - " + NetworkManagement.ListIP(3));
            Console.WriteLine("10. Get PrefferedDNS         - " + NetworkManagement.ListIP(4));
            Console.WriteLine("11. Get AlternateDNS         - " + NetworkManagement.ListIP(5));
            Console.WriteLine("12. BootTimeout " + changeBoot + " s         - " + ChangeBootTimeout.ChangeBoot("bcdedit/timeout " + changeBoot));
            Console.WriteLine("13. Never check for updates  - " + WnUpdatesOptions.UserAccount());
            Console.WriteLine("14. High performance         - " + ChangeBootTimeout.ChangeBoot("powercfg.exe /setactive 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c"));
            Console.WriteLine("15. Disable sleep            - " + ChangeBootTimeout.ChangeBoot("powercfg –x -standby-timeout-ac 0"));
            Console.WriteLine("16. HardDisk (Sleep->Never)  - " + ChangeBootTimeout.ChangeBoot("powercfg -Change -disk-timeout-ac 0"));
            Console.WriteLine("17. Monitor (Sleep->Never)   - " + ChangeBootTimeout.ChangeBoot("powercfg -Change -monitor-timeout-ac 0"));
            Console.WriteLine("18. Default language en-US   - " + ChangeBootTimeout.ChangeBoot("control.exe intl.cpl"));
            ChangeLanguageToEnUs.EnUs();
            Console.WriteLine("\nSome prompts require restart to be initialized.");
            Console.Write("\n\nRESTART  Y or N ,  push Enter: ");
            PromptToRestartSystem.Restart();
        }             
    }
}
