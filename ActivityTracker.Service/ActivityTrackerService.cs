using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ActivityTracker.Service
{
    public partial class ActivityTrackerService : ServiceBase
    {
        public ActivityTrackerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Listener.Init();
        }

        protected override void OnStop()
        {
        }
    }
}
