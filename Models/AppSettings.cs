using IWshRuntimeLibrary;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace PowerTray.Models
{
    public class AppSettings
    {
        private static readonly string FileName = string.Concat(Process.GetCurrentProcess().ProcessName, ".exe");
        private static readonly string LinkName = string.Concat(Process.GetCurrentProcess().ProcessName, ".lnk");
        private static readonly string AppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PowerTray");
        private static readonly string StartupDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        private static readonly string CurrentDirectory = Environment.CurrentDirectory;
        private static readonly string AppDataFile = Path.Combine(AppDataDirectory, FileName);
        private static readonly string StartupLink = Path.Combine(StartupDirectory, LinkName);
        private static readonly string CurrentFile = Path.Combine(CurrentDirectory, FileName);
        private static readonly RegistryKey rootkey = Registry.CurrentUser.OpenSubKey(@"Software\", true);
        private static readonly RegistryKey appkey = rootkey.OpenSubKey("PowerTray", true) ?? rootkey.CreateSubKey("PowerTray");

        public static bool ForceShutdownEnabled
        {
            get
            {
                
                var value = (int)appkey.GetValue("ForceShutdownEnabled");
                return value == 1;
            }
            set
            {
                appkey.SetValue("ForceShutdownEnabled", value ? 1 : 0);
            }
        }

        public static bool ForceRestartEnabled
        { 
            get
            {
                var value = (int)appkey.GetValue("ForceRestartEnabled");
                return value == 1;
            }
            set
            {
                appkey.SetValue("ForceRestartEnabled", value ? 1 : 0);
            }
        }

        public static bool StartWithOSEnabled
        {
            get
            {
                var value = (int)appkey.GetValue("StartWithOSEnabled");
                return value == 1;
            }
            set
            {
                appkey.SetValue("StartWithOSEnabled", value ? 1 : 0);
            }
        }

        public AppSettings()
        {
            if (appkey.ValueCount == 0)
            {
                appkey.SetValue("ForceShutdownEnabled", 1);
                appkey.SetValue("ForceRestartEnabled", 1);
                appkey.SetValue("StartWithOSEnabled", 1);
            }
        }

        public static void CopyApplicationToAppData()
        {
            // Don't copy if already running from AppData
            if (CurrentDirectory.Equals(AppDataDirectory)) return;

            Directory.CreateDirectory(AppDataDirectory);
            System.IO.File.Copy(CurrentFile, AppDataFile, true);
        }

        public static void CreateStartupShortcut()
        {
            // Don't create shortcut if it already exists
            if (System.IO.File.Exists(StartupLink)) return;

            // Create shortcuts only if program exists in AppData
            if (System.IO.File.Exists(AppDataFile))
            {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(StartupLink);

                shortcut.TargetPath = AppDataFile;
                shortcut.WorkingDirectory = AppDataDirectory;
                shortcut.Save();
            }
        }

        public static void RemoveStartupShortcut()
        {
            if (System.IO.File.Exists(StartupLink)) System.IO.File.Delete(StartupLink);
        }
    }
}
