using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityTracker.Core
{
    public class Tracker
    {
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ActivityTracker";
        private string file = "ActivityTracker.json";

        private TrackerLog _CurrentTracker;

        private List<TrackerLog> _Logs { get; set; }

        public Tracker()
        {
            
        }

        public TimeSpan GetToDayActivity()
        {
            var tmp = _Logs.Where(l => l.LogIn.Value.Date == DateTime.Now.Date);
            if (tmp.Any())
                return new TimeSpan(tmp.Select(s => (s.LogOut == null ? DateTime.Now : s.LogOut) - s.LogIn).Sum(s => s.Value.Ticks));
            return DateTime.Now - DateTime.Now.Date;
        }

        public TimeSpan GetSessionActivity()
        {
            var tmp = _Logs.Where(l => l.LogIn.Value.Date == DateTime.Now.Date);
            if (tmp.Any())
            {
                var time = (tmp.LastOrDefault().LogOut == null ? DateTime.Now : tmp.LastOrDefault().LogOut) - tmp.LastOrDefault().LogIn;
                return time.Value;
            }
            else
            {
                tmp = _Logs.Where(l => l.LogIn.Value.Date == DateTime.Now.Date.AddDays(-1));
                if (tmp.Any() && tmp.LastOrDefault().LogOut == null)
                {
                    var time = DateTime.Now - tmp.LastOrDefault().LogIn.Value;
                    return time;
                }
            }

            return new TimeSpan();
        }


        public void Init()
        {
            _Logs = new List<TrackerLog>();
            if (!File.Exists(path + "\\" + file))
            {
                var f = File.Create(path + "\\" + file);
                f.Close();
                _Logs = new List<TrackerLog>();
            }
            else
            {
                var jsonString = File.ReadAllText(path + "\\" + file);
                _Logs = JsonConvert.DeserializeObject<List<TrackerLog>>(jsonString);
            }

            if (_Logs.Any() && _Logs.LastOrDefault()?.LogOut == null)
                _CurrentTracker = _Logs.LastOrDefault();
            else
                Start();
        }

        public void Start()
        {
            if (_CurrentTracker != null)
                throw new Exception("Tracker is already started");

            _CurrentTracker = new TrackerLog();
            _CurrentTracker.LogIn = DateTime.Now;
            _Logs.Add(_CurrentTracker);

            startedOccured();
            Save();
        }


        public void Stop()
        {
            _CurrentTracker.LogOut = DateTime.Now;

            stopedOccured();

            _CurrentTracker = null;
            Save();
        }

        public void Save()
        {
            File.WriteAllText(path + "\\" + file, JsonConvert.SerializeObject(_Logs));
        }

        public event EventHandler Started;
        public event EventHandler Stoped;

        private void startedOccured()
        {
            if (Started != null)
                Started(null, new EventArgs());
        }

        private void stopedOccured()
        {
            if (Stoped != null)
                Stoped(null, new EventArgs());
        }
        public TrackerLog GetTracker()
        {
            if (_CurrentTracker == null)
                throw new Exception("Tracker Not Started !");

            return _CurrentTracker;
        }
    }
}
