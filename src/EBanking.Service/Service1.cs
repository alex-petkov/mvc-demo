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
using EBanking.Data;

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
           _timer = new Timer(Interest, null, 10000, 1000);
            
           
        }

        protected void Interest(object state)
        {
            Work();
        }
        protected override void OnStop()
        {
            _timer.Dispose();
        }

        public static void Work()
        {
            using (var db = new OurDbContext())
            {
                db.ApplyInterest(0.01m);

            }
        }
    }
}
