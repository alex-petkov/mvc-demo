using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EBanking.Service
{
    public partial class Service1 : ServiceBase
    {
        private Timer _timer;

        public Service1()
        {
            //System.Threading.Timer
            //ConfigurationManager.AppSettings

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {
        }
    }
}
