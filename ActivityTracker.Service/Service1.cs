using ActivityTracker.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ActivityTracker.Service
{
    public partial class ActivityTracker : ServiceBase
    {
        public ActivityTracker()
        {
            InitializeComponent();
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanStop = true;
        }
        

        protected override void OnStart(string[] args)
        {
            _Tracker = new Tracker();
            _Tracker.Init();
        }

        protected override void OnShutdown()
        {
            _Tracker.Stop();
            base.OnShutdown();
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            if (changeDescription.Reason == SessionChangeReason.SessionLogon|| changeDescription.Reason == SessionChangeReason.SessionUnlock)
            {
                _Tracker.Start();
            }
            else if (changeDescription.Reason == SessionChangeReason.SessionLogoff || changeDescription.Reason == SessionChangeReason.SessionLock)
            {
                _Tracker.Stop();
            }

            base.OnSessionChange(changeDescription);
        }

        private Tracker _Tracker;

    }
}
