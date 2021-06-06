using System;
using System.Configuration;
using System.IO;

namespace Persistence
{
    public static class FileIo
    {
        private const string FileExt = ".json";

        internal static string Read(string fileName)
        {
            var directory = DirectorySetting.GetWorkingDirectory();
            var path = $"{directory}{Path.DirectorySeparatorChar}{fileName}{FileExt}";

            if (File.Exists(path))
            {
                try
                {
                    var text = File.ReadAllText(path);
                    return text;
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                File.Create(path);
            }
            return string.Empty;
        }

        internal static void Write(string fileName, string contents)
        {
            var directory = DirectorySetting.GetWorkingDirectory();

            var path = Path.EndsInDirectorySeparator(directory)
                ? $"{directory}{fileName}{FileExt}"
                : $"{directory}{Path.DirectorySeparatorChar}{fileName}{FileExt}";

            File.WriteAllText(path, contents);
        }

    }
}
