using PowerTray.Models;
using System;

namespace PowerTray.Views
{
    public partial class PowerButtonsUserControl
    {
        public PowerButtonsUserControl()
        {
            InitializeComponent();
        }

        private void Shutdown_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PowerSettings.Shutdown();
            Environment.Exit(0);
        }

        private void Restart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PowerSettings.Restart();
            Environment.Exit(0);
        }

        private void Sleep_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PowerSettings.Sleep();
        }

        private void Hibernate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PowerSettings.Hibernate();
        }

        private void Logoff_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PowerSettings.Logoff();
            Environment.Exit(0);
        }

        private void Lock_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PowerSettings.Lock();
        }
    }
}
