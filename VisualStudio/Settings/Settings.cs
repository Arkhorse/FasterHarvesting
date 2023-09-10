namespace FasterHarvesting
{
    public class Settings : JsonModSettings
    {
        internal static Settings Instance { get; } = new();

        [Section("SETTING_SECTION_GENERAL", Localize = true)]

        [Name("SETTING_GENERAL_REMEMBER_BREAKDOWN", Localize = true)]
        public bool RememberBreakDown = false;

        [Section("SETTING_SECTION_LIMB", Localize = true)]

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

        [Section("SETTING_SECTION_CUSTOM_LIST", Localize = true)]

        [Name("SETTING_CUSTOM_LIST_ENABLE", Localize = true)]
        [Description("SETTING_CUSTOM_LIST_ENABLE_DESC", Localize = true)]
        public bool AllowCustomList = false;

        [Section("SETTING_SECTION_ADVANCED", Localize = true)]

        [Name("SETTING_ADVANCED_LOG_INTERACTIVE", Localize = true)]
        [Description("SETTING_ADVANCED_LOG_INTERACTIVE_DESC", Localize = true)]
        public bool InteractiveLog = false;

        [Name("SETTING_ADVANCED_LOG_CUSTOM_LIST", Localize = true)]
        public bool CustomListHandlerDebug = false;

        [Name("SETTING_ADVANCED_LOG_DEFINITION", Localize = true)]
        public bool LogDefinition = false;

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