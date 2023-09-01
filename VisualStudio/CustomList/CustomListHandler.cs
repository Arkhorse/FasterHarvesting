using System.Security.AccessControl;
using System.Text.Json;

namespace FasterHarvesting.CustomList
{
    internal class CustomListHandler
    {
        private static ICustomListEntry demo = new("INTERACTIVE_BranchA_Prefab",1.5d);

        public static void Initilize()
        {
            if (!Settings.Instance.AllowCustomList) return;
            PopulateConfigFiles();
        }

        public static void PopulateConfigFiles()
        {
            try
            {
                string[] files = Directory.GetFiles(Main.CustomListFolder, "*.json");
                Array.Sort(files, StringComparer.InvariantCultureIgnoreCase);

                foreach (string file in files)
                {
                    LoadConfigFile(file);
                }

            }
            catch (FileNotFoundException fnf)
            {
                Logger.LogError($"Custom list file not found. Reason: {fnf.Message} {fnf.StackTrace}");
            }
            catch (Exception e)
            {
                Logger.LogError($"Unknown error prevented the Custom List from being populated. Exception: {e.Message} {e.StackTrace}");
            }
        }

        public static void LoadConfigFile(string configFileName)
        {
            string ConfigFilePath = Path.Combine(Main.CustomListFolder, configFileName);

            try
            {
                using FileStream CustomFileStream = File.OpenRead(ConfigFilePath);

                ICustomListEntry? entry = JsonSerializer.Deserialize<ICustomListEntry>(CustomFileStream);

                if (VerifyEntry(entry))
                {
                    Logger.Log($"Entry added with path {configFileName}");
                    Main.entries.Add(entry!);
                }
                else
                {
                    Logger.LogWarning(
                        $"Config returned false: {configFileName}\nIs Null: {entry == null}\n If entry is not null, then it already exists in the Default Objects list"
                        );
                }

                CustomFileStream.Dispose();
            }
            catch (UnauthorizedAccessException accessdenied)
            {
                Logger.LogError($"Access was denied when attempting to load the config file named {configFileName}");
                throw accessdenied;
            }
            catch
            {
                Logger.LogError($"Unknown error prevented the config file named {configFileName} from loading");
                throw;
            }
        }

        public static bool VerifyEntry(ICustomListEntry? entry)
        {
            if (entry != null)
            {
                if (Main.DefaultObjects.Exists(d => d.ObjectName == entry.ObjectName))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return entry != null;
        }

        public static void SaveDemo()
        {
            try
            {
                using FileStream demostream = File.OpenWrite(Main.DemoFile);

                JsonSerializer.Serialize<ICustomListEntry>(demo, new JsonSerializerOptions() { WriteIndented = true, IncludeFields = true });
                demostream.Dispose();
            }
            catch (UnauthorizedAccessException accessdenied)
            {
                Logger.LogError($"Access was denied when attempting to save the demo config file");
                throw accessdenied;
            }
            catch
            {
                Logger.LogError($"Unknown error prevented the demo file from saving");
                throw;
            }
        }
    }
}
