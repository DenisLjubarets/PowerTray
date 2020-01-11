using Microsoft.Win32;
using System.Diagnostics;

namespace PowerTray.Models
{
    public static class RegistrySettings
    {
        private static readonly string AppName = Process.GetCurrentProcess().ProcessName;
        private static readonly string ForceShutdownValueName = "ForceShutdown";
        private static readonly string ForceRestartValueName = "ForceRestart";
        private static readonly string StartWithOSValueName = "StartWithOS";

        public static int ForceShutdownValue
        {
            get
            {
                return GetValue(ForceShutdownValueName, 1);
            }
            set
            {
                SetValue(ForceShutdownValueName, value);
            }
        }

        public static int ForceRestartValue
        {
            get
            {
                return GetValue(ForceRestartValueName, 1);
            }
            set
            {
                SetValue(ForceRestartValueName, value);
            }
        }

        public static int StartWithOSValue
        {
            get
            {
                return GetValue(StartWithOSValueName, 1);
            }
            set
            {
                SetValue(StartWithOSValueName, value);
            }
        }

        private static void SetValue(string valueName, int value)
        {
            using (var rootKey = Registry.CurrentUser.OpenSubKey(@"Software\", true))
            {
                using (var appKey = rootKey.OpenSubKey(AppName, true) ?? rootKey.CreateSubKey(AppName))
                {
                    appKey.SetValue(valueName, value);
                }
            }
        }

        private static int GetValue(string valueName, int defaultValue)
        {
            using (var rootKey = Registry.CurrentUser.OpenSubKey(@"Software\", true))
            {
                using (var appKey = rootKey.OpenSubKey(AppName, true) ?? rootKey.CreateSubKey(AppName))
                {
                    return (int)appKey.GetValue(valueName, defaultValue);
                }
            }
        }
    }
}
