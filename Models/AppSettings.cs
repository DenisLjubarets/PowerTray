using System;
using System.Diagnostics;
using System.IO;

namespace PowerTray.Models
{
    public static class AppSettings
    {
        private static readonly string AppName = Process.GetCurrentProcess().ProcessName;
        private static readonly string FileName = string.Concat(AppName, ".exe");
        private static readonly string LinkName = string.Concat(AppName, ".lnk");
        private static readonly string AppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName);
        private static readonly string CurrentDirectory = Environment.CurrentDirectory;
        private static readonly string AppDataFile = Path.Combine(AppDataDirectory, FileName);
        private static readonly string StartupLink = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), LinkName);
        private static readonly string CurrentFile = Path.Combine(CurrentDirectory, FileName);

        public static bool ForceShutdownEnabled
        {
            get
            {
                return RegistrySettings.ForceShutdownValue == 1;
            }
            set
            {
                RegistrySettings.ForceShutdownValue = value ? 1 : 0;
            }
        }

        public static bool ForceRestartEnabled
        { 
            get
            {
                return RegistrySettings.ForceRestartValue == 1;
            }
            set
            {
                RegistrySettings.ForceRestartValue = value ? 1 : 0;
            }
        }

        public static bool StartWithOSEnabled
        {
            get
            {
                return RegistrySettings.StartWithOSValue == 1;
            }
            set
            {
                RegistrySettings.StartWithOSValue = value ? 1 : 0;
            }
        }

        public static void CopyApplicationToAppData()
        {
            // Don't copy if already running from AppData
            if (CurrentDirectory.Equals(AppDataDirectory)) return;

            // Close AppData application
            CloseAppDataFile();

            // Create application's directory in AppData if it doesn't exist
            Directory.CreateDirectory(AppDataDirectory);

            // Copy current app to AppData
            File.Copy(CurrentFile, AppDataFile, true);
        }

        private static void CloseAppDataFile()
        {
            // Loop over each process
            foreach (Process process in Process.GetProcesses())
            {
                string processName = process.ProcessName;

                // If process name equals to current process name
                if (processName.Equals(AppName))
                {
                    string processFile = process.MainModule.FileName;

                    // If process runs from AppData
                    if (processFile.Equals(AppDataFile))
                    {
                        process.Kill();
                        process.WaitForExit();
                    }
                }
            }
        }

        public static void CreateStartupShortcut()
        {
            // Don't create shortcut if it already exists
            if (File.Exists(StartupLink)) return;

            // Create shortcuts only if program exists in AppData
            if (File.Exists(AppDataFile))
            {
                var shell = new IWshRuntimeLibrary.WshShell();
                var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(StartupLink);

                shortcut.TargetPath = AppDataFile;
                shortcut.WorkingDirectory = AppDataDirectory;
                shortcut.Save();
            }
        }

        public static void RemoveStartupShortcut()
        {
            if (File.Exists(StartupLink)) File.Delete(StartupLink);
        }
    }
}
