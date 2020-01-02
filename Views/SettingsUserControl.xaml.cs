using PowerTray.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PowerTray.Views
{

    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();

            CheckBoxForceShutdown.IsChecked = AppSettings.ForceShutdownEnabled;
            CheckBoxForceRestart.IsChecked = AppSettings.ForceRestartEnabled;
            CheckBoxStartWithOS.IsChecked = AppSettings.StartWithOSEnabled;
        }

        private void CheckBoxForceShutdown_Changed(object sender, RoutedEventArgs e)
        {
            AppSettings.ForceShutdownEnabled = CheckBoxForceShutdown.IsChecked ?? false;
        }

        private void CheckBoxForceRestart_Changed(object sender, RoutedEventArgs e)
        {
            AppSettings.ForceRestartEnabled = CheckBoxForceRestart.IsChecked ?? false;
        }

        private void CheckBoxStartWithOS_Changed(object sender, RoutedEventArgs e)
        {
            AppSettings.StartWithOSEnabled = CheckBoxStartWithOS.IsChecked ?? false;
            if (AppSettings.StartWithOSEnabled)
            {
                AppSettings.CreateStartupShortcut();
            }
            else
            {
                AppSettings.RemoveStartupShortcut();
            }
        }

        private void Quit_Clicked(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

    }
}
