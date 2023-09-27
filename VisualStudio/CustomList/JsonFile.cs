using System.Text.Json;
using FasterHarvesting.Utilities;

namespace FasterHarvesting.CustomList
{
    public class JsonFile
    {
        /// <summary>
        /// Loads and existing config
        /// </summary>
        /// <param name="configFileName">The full path to the file with extension</param>
        public static async void Load(string configFileName)
        {
            if (string.IsNullOrEmpty(configFileName) || configFileName == Main.DemoConfigFile) return;

            await LoadConfig(configFileName);
        }

        /// <summary>
        /// Saves an entry to a config
        /// </summary>
        /// <param name="entry">The <see cref="ICustomListEntry"/> to save to the json file</param>
        public static async void Save(ICustomListEntry entry)
        {
            await SaveConfig(entry);
        }

        #region tasks

        private static async Task LoadConfig(string configFileName)
        {
            try
            {
                using FileStream CustomFileStream = File.Open(configFileName, FileMode.Open, FileAccess.Read, FileShare.Read);

                ICustomListEntry entry = await JsonSerializer.DeserializeAsync<ICustomListEntry>(CustomFileStream, Main.options);

                Main.ObjectsToAlter.Add(entry);

                await CustomFileStream.DisposeAsync();
            }
            catch (UnauthorizedAccessException accessdenied)
            {
                Logging.LogError($"Access was denied when attempting to load the config file named {configFileName}");
                throw accessdenied;
            }
            catch
            {
                Logging.LogError($"Unknown error prevented the config file named {configFileName} from loading");
                throw;
            }
        }

        private static async Task SaveConfig(ICustomListEntry entry)
        {
            string path = Path.Combine(Main.CustomListFolder, $"{entry.ObjectName}.json");
            if (File.Exists(path)) return;

            try
            {
                using (FileStream file = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await JsonSerializer.SerializeAsync<ICustomListEntry>(file, entry, Main.options);

                    File.SetCreationTime(path, File.GetCreationTime(path));
                    File.SetLastAccessTime(path, File.GetLastAccessTime(path));
                    File.SetLastWriteTime(path, File.GetLastWriteTime(path));

                    await file.DisposeAsync();
                }
            }
            catch (Exception e)
            {
                Logging.LogError($"Could not save the config file async, {e.Message}");
            }
        }

        #endregion
    }
}
