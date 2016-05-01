using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityTracker
{
    class Listener
    {
        internal static void Init()
        {
            Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            _Tracker = new Tracker();
            _Tracker.Init();
        }
        
        private static Tracker _Tracker;

        public static Tracker GetTracker()
        {
            return _Tracker;
        }

        private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLogon || e.Reason == SessionSwitchReason.SessionUnlock)
            {
                _Tracker.Start();
            }
            else if (e.Reason == SessionSwitchReason.SessionLogoff || e.Reason == SessionSwitchReason.SessionLock)
            {
                _Tracker.Stop();
            }
        }
    }
}
 