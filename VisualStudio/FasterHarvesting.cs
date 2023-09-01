using FasterHarvesting.CustomList;
using MelonLoader.Utils;

namespace FasterHarvesting
{
    public class Main : MelonMod
    {
        internal static List<ICustomListEntry> entries = new();
        internal static string CustomListFolder = Path.Combine(MelonEnvironment.ModsDirectory, "FasterHarvesting");
        internal static string DemoFile = Path.Combine(CustomListFolder, "demo.json");
        internal static bool LogInteractiveObjectDetails { get; set; }
        internal static List<ICustomListEntry> DefaultObjects { get; } = new()
        {
            new ICustomListEntry("INTERACTIVE_LimbA_Prefab",        (float)Settings.Instance.limbATime),
            new ICustomListEntry("INTERACTIVE_LimbB_Prefab",        (float)Settings.Instance.limbBTime),
            new ICustomListEntry("INTERACTIVE_LimbA_Big_Prefab",    (float)Settings.Instance.limbABigTime),
            new ICustomListEntry("INTERACTIVE_LimbB_Big_Prefab",    (float)Settings.Instance.limbBBigTime),
            new ICustomListEntry("INTERACTIVE_BranchA_Prefab",      (float)Settings.Instance.branchATime)
        };

        public override void OnInitializeMelon()
        {
            Settings.OnLoad();
            CustomListHandler.Initilize();

            uConsole.RegisterCommand("CreateDemoFile", new Action(CreateDemoFile));
        }

        public static void CreateDemoFile()
        {
            CustomListHandler.SaveDemo();
            uConsole.Log($"Demo config file saved to {DemoFile}");
        }
    }
}
