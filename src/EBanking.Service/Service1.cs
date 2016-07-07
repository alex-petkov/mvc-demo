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
            using (var db = new OurDbContext())
            {
                var usr = db.UserAccounts.Where(x=>x.Balance>=0).ToList();
                foreach (var item in usr)
                {
                    item.Balance = item.Balance + 1;
                }

            }

        }
        protected override void OnStop()
        {
            _timer.Dispose();
        }
    }
}
