using System;
using System.Configuration;
using System.IO;

namespace Persistence
{
    public static class FileIo
    {
        private const string DirectorySetting = "Directory";
        private const string FileExt = ".json";

        internal static string Read(string fileName)
        {
            var directory = GetDirectory();
            var path = $"{directory}{Path.DirectorySeparatorChar}{fileName}{FileExt}";

            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                File.Create(path);
                return string.Empty;
            }
        }

        internal static void Write(string fileName, string contents)
        {
            var directory = GetDirectory();
            var path = $"{directory}{Path.DirectorySeparatorChar}{fileName}{FileExt}";
            File.WriteAllText(path, contents);
        }

        private static string GetDirectory()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var directory = appSettings[DirectorySetting];
            if (string.IsNullOrWhiteSpace(directory))
            {
                throw new Exception("No directory configured!");
            }

            return directory;
        }

    }
}
