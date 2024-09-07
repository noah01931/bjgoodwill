using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JHEMR.JHServicesLib.Provider;

namespace JHEMR.JHAutoUpdateServiceLib
{
    class Program
    {
        static void Main(string[] args)
        {
            StartAllService();
           // JHEMR.JHServicesLib.ErrorHandler.JHLOGClientErrorHandler.Instance.Start();
        }
        private static void StartAllService()
        {
            JHSRVServiceManager service = new JHSRVServiceManager();
            service.StartServiceHost(typeof(IJHAutoUpdateService), typeof(JHEMR.JHAutoUpdateServiceLib.JHAutoUpdateService), "JHAutoUpdateService");
            service.GetConsole();
        }
    }
}
