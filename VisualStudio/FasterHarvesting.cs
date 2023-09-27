using FasterHarvesting.CustomList;
using FasterHarvesting.Utilities;
using MelonLoader.Utils;
using System.Text.Json;

namespace FasterHarvesting
{
    public class Main : MelonMod
    {
        #region Constants
        internal static string CustomListFolder { get; } = Path.Combine(MelonEnvironment.ModsDirectory, "FasterHarvesting");
        internal static string DevelopementFolder { get; } = @"P:\Modding\The Long Dark\MyMods\FasterHarvesting\VisualStudio\Configs";
        #endregion
        #region CustomList
        public static List<ICustomListEntry> ObjectsToAlter { get; set; } = new();
        #endregion
        #region Demo File
        internal static string DemoConfigFile { get; } = @"demo.json";
        internal static string DemoFile { get; } = Path.Combine(CustomListFolder, DemoConfigFile);
        #endregion
        #region Settings
        internal static bool LogInteractiveObjectDetails { get; set; }
        #endregion
        #region JsonFile
        internal static JsonSerializerOptions options { get; } = new JsonSerializerOptions() { IncludeFields = true, WriteIndented = true };
        internal static MainConfig Config { get; } = new MainConfig();
        #endregion
        public static float BreakDownCalories { get; set; }

        public override void OnInitializeMelon()
        {
            Settings.OnLoad();
            CustomListHandler.Initilize();

            uConsole.RegisterCommand("CreateDemoFile", new Action(CreateDemoFile));
            uConsole.RegisterCommand("ReloadConfigFiles", new Action(ReloadConfigFiles));
        }

        public override void OnApplicationQuit()
        {
            base.OnApplicationQuit();

            CopyConfigFilesToDev();
        }

        public static void CopyConfigFilesToDev()
        {
            if (Directory.Exists(DevelopementFolder) && Settings.Instance.CopyFilesToDev)
            {
                try
                {
                    string[] Files = Directory.GetFiles(CustomListFolder, "*.json");
                    foreach (string file in Files)
                    {
                        if (File.Exists(Path.Combine(DevelopementFolder, file))) continue;
                        File.Copy(file, Path.Combine(DevelopementFolder, file));
                    }
                }
                catch
                {
                    Logging.LogWarning($"Copying files to dev failed");
                    throw;
                }
            }
        }

        public static void CreateDemoFile()
        {
            CustomListHandler.SaveDemo();
            uConsole.Log($"Demo config file saved to {DemoFile}");
        }

        public static void ReloadConfigFiles()
        {
            CustomListHandler.ReInitilize();
            uConsole.Log("Config files reloaded for Faster Harvesting");
        }

        public static void ForceClosePanel_BreakDown(Panel_BreakDown breakDown)
        {
            breakDown.Enable(false);
        }

        public static float GetBreakDownTimeMinutes(float time)
        {
            return time * 60;
        }

        public static bool ObjectExists(string name)
        {
            if (ObjectsToAlter.Exists(d => d.ObjectName == name))
            {
                return true;
            }
            return false;
        }
    }
}
