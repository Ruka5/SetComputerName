using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;


namespace SetComputerName
{
    public static class ChangeLanguageToEnUs
    {
        public static string EnUs()
        {
            CultureInfo current = CultureInfo.CurrentCulture;
            if (!current.Name.Equals("en-US"))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");
            }
            return "OK";
        }
    }
}
