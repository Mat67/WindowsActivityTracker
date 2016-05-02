using ActivityTracker.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityTracker.Service
{
    class Listener
    {
        internal static void Init()
        {
            Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            SystemEvents.SessionEnded += SystemEvents_SessionEnded;
            _Tracker = new Tracker();
            _Tracker.Init();
        }

        private static void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
        {
            _Tracker.Stop();
        }
        

        private static Tracker _Tracker;

        public static Tracker GetTracker()
        {
            return _Tracker;
        }

        private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            var path = @"C:\ADHOC-GTI";
            //var path = @"C:\Users\Mathieu\AppData\Roaming\ActivityTracker";
            var file = "track.txt";

            File.WriteAllText(path + "\\" + file, e.Reason.ToString());


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
 