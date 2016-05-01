using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ActivityTracker
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {

        MainWindow _Windows = new ActivityTracker.MainWindow();

        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += Current_DispatcherUnhandledException;

            // Initialise l'icone dans le system tray
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon(@"Icones\chrono.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    _Windows.Show();
                    _Windows.WindowState = WindowState.Normal;
                };

            // Initialise le listener
            Listener.Init();

        }

        private void Current_DispatcherUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ActivityTracker";
            var file = "exception.txt";

            File.WriteAllText(path + "\\" + file, (e.ExceptionObject as Exception).Message);
        }
    }
}
