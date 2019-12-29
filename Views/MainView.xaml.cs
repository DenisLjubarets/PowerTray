using PowerTray.Models;

namespace PowerTray.Views
{
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
            AppSettings.CopyApplicationToAppData();
        }
    }
}
