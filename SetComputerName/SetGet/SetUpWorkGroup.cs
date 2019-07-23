using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Management.Instrumentation;


namespace SetComputerName
{
    //Name of workgroup
    class SetUpWorkGroup
    {
        public static string SetGroupName(string name)
        {
            try
            {
                //"MODULE"
                ManagementObject manage = new ManagementObject(string.Format("Win32_ComputerSystem.Name='{0}'", Environment.MachineName));
                object[] args = { name, null, null, null };

                manage.InvokeMethod("JoinDomainOrWorkgroup", args);

                return args[0].ToString();
            }
            catch (ArgumentNullException)
            {
                return "Write some a name.";
            }
            catch (FormatException e)
            {
                return e.Message;
            }
        }
    }
}
