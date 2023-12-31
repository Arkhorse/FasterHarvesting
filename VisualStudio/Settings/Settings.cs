namespace FasterHarvesting
{
    public class Settings : JsonModSettings
    {
        internal static Settings Instance { get; } = new();

        [Name("GAMEPLAY_BranchBigSoft", Localize = true)]
        [Slider(0.01f, 12.00f, NumberFormat = "{0:F2}")]
        public float limbATime = 1.50f;

        [Name("GAMEPLAY_BranchBigHard", Localize = true)]
        [Slider(0.01f, 12.00f, NumberFormat = "{0:F2}")]
        public float limbBTime = 1.50f;

        [Name("GAMEPLAY_BranchBigSoft", Localize = true)]
        [Slider(0.01f, 12.00f, NumberFormat = "{0:F2}")]
        public float limbABigTime = 1.50f;

        [Name("GAMEPLAY_BranchBigHard", Localize = true)]
        [Slider(0.01f, 12.00f, NumberFormat = "{0:F2}")]
        public float limbBBigTime = 1.50f;

        [Name("GAMEPLAY_branch", Localize = true)]
        [Slider(0.01f, 12.00f, NumberFormat = "{0:F2}")]
        public float branchATime = 0.170f;

        [Section("Custom List")]

        [Name("Allow Custom List")]
        [Description("Please see the mods repo on how to use this function")]
        public bool AllowCustomList = false;

        [Section("Advanced Options")]

        [Name("Enable Interactive Object Log")]
        [Description("This will log the currently active object data. Only enable this if you need this info")]
        public bool InteractiveLog = false;

        [Name("Log CustomListHandler debug")]
        public bool CustomListHandlerDebug = false;

        protected override void OnConfirm()
        {
            base.OnConfirm();

            Main.LogInteractiveObjectDetails = InteractiveLog;
        }

        // This is used to load the settings
        internal static void OnLoad()
        {
            Instance.AddToModSettings(BuildInfo.GUIName);
            Instance.RefreshGUI();
        }
    }
}