using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace SongzaClasses
{
    public static class Settings
    {
        private const string FileName = "settings.json";
        private const string FolderName = "SongzaDesktop";

        public static string Username { get; set; }
        public static string UserId { get; set; }
        public static string Password { get; set; }
        public static string SessionId { get; set; }
        public static DateTime Created { get; set; }

        static Settings()
        {
            string folderPath = string.Format("{0}\\{1}",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                FolderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            Load();
        }

        public static void Load()
        {
            string folderPath = string.Format("{0}\\{1}",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                FolderName);
            string filePath = string.Format("{0}\\{1}",
                folderPath,
                FileName);

            if (File.Exists(filePath))
            {
                using (var sw = new StreamReader(new FileStream(filePath, FileMode.Open)))
                {
                    var settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(sw.ReadToEnd());
                    var vals = typeof(Settings).GetProperties();

                    foreach (var val in vals)
                    {
                        if (settings.ContainsKey(val.Name))
                        {
                            val.SetValue(null, Convert.ChangeType(settings[val.Name], val.PropertyType));
                        }
                    }
                }
            }
        }

        public static void Save()
        {
            string folderPath = string.Format("{0}\\{1}",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                FolderName);
            string filePath = string.Format("{0}\\{1}",
                folderPath,
                FileName);

            using (var sw = new StreamWriter(new FileStream(filePath, FileMode.Create)))
            {
                var settings = new ExpandoObject() as IDictionary<string, object>; ;
                var vals = typeof (Settings).GetProperties();

                foreach (var val in vals)
                {
                    settings[val.Name] = val.GetValue(null);
                }

                sw.WriteLine(JsonConvert.SerializeObject(settings));
            }
        }
    }
}