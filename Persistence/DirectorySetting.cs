using BikeCommon;
using System;
using System.Configuration;

namespace Persistence
{
    public static class DirectorySetting
    {
        private const string DirectorySettingName = "Directory";

        public static string GetWorkingDirectory()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var directory = appSettings[DirectorySettingName];
            if (string.IsNullOrWhiteSpace(directory))
            {
                throw new BikeException("No directory configured!");
            }

            return directory;
        }

        public static void SetWorkingDirectory(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                throw new BikeException("Must give a valid directory!");
            }
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Clear();
            config.AppSettings.Settings.Add(DirectorySettingName, directory);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            Console.WriteLine($"New directory: {directory}");
        }

    }
}
