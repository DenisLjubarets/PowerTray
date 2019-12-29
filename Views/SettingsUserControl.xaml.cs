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

            CheckBoxForceShutdown.IsChecked = Properties.Settings.Default.ForceShutdownEnabled;
            CheckBoxForceRestart.IsChecked = Properties.Settings.Default.ForceRestartEnabled;
            CheckBoxStartWithOS.IsChecked = AppSettings.StartWithWindows;
        }

        private void CheckBoxForceShutdown_Changed(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ForceShutdownEnabled = CheckBoxForceShutdown.IsChecked ?? false;
            Properties.Settings.Default.Save();
        }

        private void CheckBoxForceRestart_Changed(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ForceRestartEnabled = CheckBoxForceRestart.IsChecked ?? false;
            Properties.Settings.Default.Save();
        }

        private void CheckBoxStartWithOS_Changed(object sender, RoutedEventArgs e)
        {
            AppSettings.StartWithWindows = CheckBoxStartWithOS.IsChecked ?? false;
        }

        private void Quit_Clicked(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
