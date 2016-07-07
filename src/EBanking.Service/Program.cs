using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var command = (String.Concat(args) ?? string.Empty).Trim();

            if (command == "-noservice")
            {
                Service1.Work();
            }
            else
            {
                
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new Service1() 
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
