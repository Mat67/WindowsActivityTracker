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

namespace ActivityTracker
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

        public new void Show()
        {
            //var trackerLogs = Listener.GetTrackerLogs();
            //var toDayLogs = trackerLogs.Where(t => t.LogIn.Value.Date == DateTime.Now.Date).ToList();
            //var activeTime = toDayLogs.Select(s => s.LogOut - s.LogIn).Sum(s => s.Value.TotalMinutes);
            //text.Text = activeTime.ToString();
            var today = Listener.GetTracker().GetToDayActivity();
            var current = Listener.GetTracker().GetSessionActivity();

            Today.Text = $"Today : { today.Hours }:{today.Minutes}";
            Session.Text = $"Current : { current.Hours }:{current.Minutes}";


            base.Show();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Collapsed;
        }
    }
}
