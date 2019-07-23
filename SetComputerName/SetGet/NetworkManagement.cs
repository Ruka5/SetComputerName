using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading;

namespace SetComputerName
{
    class NetworkManagement
    {
        //Get the TCP of NIC 
        //Note: Use .Net 4.0 and higher! Start as admin.
        public static string ListIP(int get)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if (!(bool)objMO["ipEnabled"])
                    continue;                
                //Console.WriteLine(objMO["Caption"] + "," + objMO["ServiceName"] + "," + objMO["MACAddress"]);
                string[] ipaddresses = (string[])objMO["IPAddress"];
                string[] subnets = (string[])objMO["IPSubnet"];
                string[] gateways = (string[])objMO["DefaultIPGateway"];
                string[] dnses = (string[])objMO["DNSServerSearchOrder"];                
                //Console.WriteLine("Printing Default Gateway Info:");
                //Console.WriteLine(objMO["DefaultIPGateway"].ToString());

                if (get == 3)
                {
                    foreach (string gw in gateways)
                        return gw;
                }
                else if (get == 1)
                {
                    foreach (string ip in ipaddresses)
                        return ip;
                }
                else if (get == 2)
                {
                    foreach (string sb in subnets)
                        return sb;
                }
                else if (get == 4)
                {
                    return dnses[0];
                }
                else if (get == 5)
                    return dnses[1];
            }
            return "";
        }

        //SetUp the TCP of NIC 
        //Note: Use .Net 4.0 and higher! Start as admin.
        public static void setIP(string IPAddress, string SubnetMask, string Gateway, string[] s)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if (!(bool)objMO["IPEnabled"])
                    continue;
                
                ManagementBaseObject objNewIP = null;
                ManagementBaseObject objSetIP = null;
                ManagementBaseObject objNewGate = null;
                ManagementBaseObject objDNS = null;

                objNewIP = objMO.GetMethodParameters("EnableStatic");
                objNewGate = objMO.GetMethodParameters("SetGateways");
                objDNS = objMO.GetMethodParameters("SetDNSServerSearchOrder");

                if (objDNS != null)
                {
                    //string[] s = { "10.122.126.11", "10.122.126.12" };
                    objDNS["DNSServerSearchOrder"] = s;
                    objMO.InvokeMethod("SetDNSServerSearchOrder", objDNS, null);
                }

                //Set DefaultGateway
                objNewGate["DefaultIPGateway"] = new string[] { Gateway };
                objNewGate["GatewayCostMetric"] = new int[] { 1 };

                //Set IPAddress and Subnet Mask
                objNewIP["IPAddress"] = new string[] { IPAddress };
                objNewIP["SubnetMask"] = new string[] { SubnetMask };

                // Set NewDns
                //objNewDns["DNS"] = new string[] { "10.122.126.11", "10.122.126.12" };
                objSetIP = objMO.InvokeMethod("EnableStatic", objNewIP, null);
                objSetIP = objMO.InvokeMethod("SetGateways", objNewGate, null);
                    
                //Console.WriteLine("Updated IPAddress, SubnetMask and Default Gateway!");                
            }
        }
    }
}
