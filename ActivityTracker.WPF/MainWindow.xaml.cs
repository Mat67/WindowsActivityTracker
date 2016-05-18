using ActivityTracker.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ActivityTracker.WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
        }


        TimeSpan _Today;
        TimeSpan _Current;
        TrackerLog _LastTrackerLog;
        DateTime _ShowDateTime;


        public new void Show()
        {
            var tracker = new Tracker();
            tracker.Init();

            _Today = tracker.GetToDayActivity();
            _Current = tracker.GetSessionActivity();
            _LastTrackerLog = tracker.GetTracker();
            _ShowDateTime = DateTime.Now;

            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            base.Show();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var current = DateTime.Now - _ShowDateTime;
            var today =  current + _Today;
            var session =  current + _Current;
            Today.Text = $"Today : { today.Hours }:{today.Minutes}:{today.Seconds}";
            Session.Text = $"Current : { session.Hours }:{session.Minutes}:{today.Seconds}";
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Collapsed;
        }
    }
}
