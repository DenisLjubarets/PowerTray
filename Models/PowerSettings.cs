using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PowerTray.Models
{
    public static class PowerSettings
    {

        [DllImport("user32")]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        [DllImport("user32")]
        public static extern void LockWorkStation();

        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        public static void Shutdown()
        {
            string args = AppSettings.ForceShutdownEnabled ? "/s /f /t 0" : "/s /t 0";
            Process.Start("shutdown", args);
        }

        public static void Restart()
        {
            string args = AppSettings.ForceRestartEnabled ? "/r /f /t 0" : "/r /t 0";
            Process.Start("shutdown", args);
        }

        public static void Sleep()
        {
            SetSuspendState(false, true, true);
        }

        public static void Hibernate()
        {
            SetSuspendState(true, true, true);
        }

        public static void Logoff()
        {
            ExitWindowsEx(0, 0);
        }

        public static void Lock()
        {
            LockWorkStation();
        }

    }
}
