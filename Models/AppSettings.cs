using IWshRuntimeLibrary;
using System;
using System.Diagnostics;
using System.IO;

namespace PowerTray.Models
{
    public static class AppSettings
    {
        private static readonly string FileName = string.Concat(Process.GetCurrentProcess().ProcessName, ".exe");
        private static readonly string LinkName = string.Concat(Process.GetCurrentProcess().ProcessName, ".lnk");
        private static readonly string AppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PowerTray");
        private static readonly string StartupDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        private static readonly string CurrentDirectory = Environment.CurrentDirectory;

        public static bool StartWithWindows
        {
            get
            {
                string shortcut = Path.Combine(StartupDirectory, LinkName);
                return System.IO.File.Exists(shortcut);
            }
            set
            {
                if (value == true)
                {
                    CopyApplicationToAppData();
                    CreateStartupShortcut();
                }
                else
                {
                    RemoveStartupShortcut();
                }
            }
        }

        public static void CopyApplicationToAppData()
        {
            if (CurrentDirectory.Equals(AppDataDirectory)) return;
            Directory.CreateDirectory(AppDataDirectory);
            string sourceFile = Path.Combine(CurrentDirectory, FileName);
            string destinationFile = Path.Combine(AppDataDirectory, FileName);
            System.IO.File.Copy(sourceFile, destinationFile, true);
        }

        private static void CreateStartupShortcut()
        {
            string shortcutPath = Path.Combine(StartupDirectory, LinkName);
            string targetPath = Path.Combine(AppDataDirectory, FileName);
            if (System.IO.File.Exists(targetPath))
            {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

                shortcut.TargetPath = targetPath;
                shortcut.Save();
            }
        }

        private static void RemoveStartupShortcut()
        {
            string shortcutPath = Path.Combine(StartupDirectory, LinkName);
            if (System.IO.File.Exists(shortcutPath)) System.IO.File.Delete(shortcutPath);
        }
    }
}
