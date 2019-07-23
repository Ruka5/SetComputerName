using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SetComputerName
{
    //SetUp the delay of loading of OS after restart
    public static class PromptToRestartSystem
    {
        public static void Restart()
        {
            string yn = Console.ReadLine();

            if (yn == "y")
                ChangeBootTimeout.ChangeBoot("shutdown -f -r -t 0");
            else
                Environment.Exit(0);
        }
    }
}
