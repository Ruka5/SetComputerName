using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using WUApiLib;

namespace SetComputerName
{
    //Automatic update disabled
    public static class WnUpdatesOptions
    {      
        public static string UserAccount()
        {           
            AutomaticUpdates auc = new AutomaticUpdates();
            auc.Settings.NotificationLevel = AutomaticUpdatesNotificationLevel.aunlDisabled;
            if (!auc.Settings.ReadOnly)
                //return OK
                auc.Settings.Save();
            return "NOK";            
        }
    }
}
