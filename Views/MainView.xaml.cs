using PowerTray.Models;
using System.ComponentModel;

namespace PowerTray.Views
{
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
            new AppSettings();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Taskbar.Visibility = System.Windows.Visibility.Collapsed;
            Taskbar.Dispose();
            base.OnClosing(e);
        }
    }
}
