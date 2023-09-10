using System.Text.Json;

namespace FasterHarvesting.CustomList
{
    internal class CustomListHandler
    {
        private static ICustomListEntry demo { get; } = new ICustomListEntry("INTERACTIVE_BranchA_Prefab", 1.5f);
        private static bool LogDebug { get; } = Settings.Instance.CustomListHandlerDebug;

        public static void Initilize()
        {
            if (!Settings.Instance.AllowCustomList) return;

            try
            {
                if (!Directory.Exists(Main.CustomListFolder))
                {
                    Directory.CreateDirectory(Main.CustomListFolder);
                }
            }
            catch
            {
                Logger.LogError("Attempting to create custom list directory failed");
                throw;
            }
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
                    if (LogDebug) Logger.Log($"Entry added with path {configFileName}");
                    Main.entries.Add(entry!);
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
                if (Main.DefaultObjects.Exists(d => d.ObjectName == entry.ObjectName) || Main.entries.Exists(d => d.ObjectName == entry.ObjectName))
                {
                    Logger.LogWarning($"Defined entry already exists, {entry.ObjectName}");
                    return false;
                }

                if (entry.ObjectBreakDownTime > 12.00f)
                {
                    entry.ObjectBreakDownTime = 12.00f;
                    Logger.LogWarning($"Entry: {entry.ToString()} has a BreakDown Time greater than 12.00d");
                }

                GameObject entryObject = GameObject.Find(entry.ObjectName);
                if (entryObject)
                {
                    if (LogDebug) Logger.Log($"entry GameObject has been found, {entry.ObjectName}");
                    Il2Cpp.BreakDown entryBreakDown = entryObject.GetComponent<Il2Cpp.BreakDown>();
                    if (entryBreakDown)
                    {
                        if (LogDebug) Logger.Log("entry has a BreakDown component");
                        return true;
                    }
                }
                else
                {
                    Logger.LogWarning($"entry GameObject has not been found, {entry.ObjectName}");
                }
            }

            return false;
        }

        public static void SaveDemo()
        {
            try
            {
                if (!File.Exists(Main.DemoFile))
                {
                    using FileStream createStream = File.Create(Main.DemoFile);
                    createStream.Dispose();
                }
            }
            catch
            {
                Logger.LogError("Atempting to creat Demo config file failed");
                throw;
            }
            try
            {
                using FileStream demostream = File.OpenWrite(Main.DemoFile);

                JsonSerializer.Serialize<ICustomListEntry>(demostream, demo, new JsonSerializerOptions() { WriteIndented = true, IncludeFields = true });
                demostream.Dispose();
            }
            catch (UnauthorizedAccessException)
            {
                Logger.LogError("Access was denied when attempting to save the demo config file");
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                Logger.LogError("Directory was not found. This catch should never be called, if it is, there is another problem occuring");
                throw;
            }
            catch
            {
                Logger.LogError("Unknown error prevented the demo file from saving");
                throw;
            }
        }
    }
}
