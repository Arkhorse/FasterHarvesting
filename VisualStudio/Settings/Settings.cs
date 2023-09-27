using FasterHarvesting.CustomList;

namespace FasterHarvesting
{
    public class Settings : JsonModSettings
    {
        internal static Settings Instance { get; } = new();

        /*[Name( "", Localize = true )]
		[Description( "", Localize = true )]*/

        [Section("SETTING_SECTION_GENERAL", Localize = true)]

        [Name("SETTING_GENERAL_PRESERVE_CALORIES", Localize = true)]
        [Description("SETTING_GENERAL_PRESERVE_CALORIES_DESC", Localize = true)]
        public bool GENERAL_PreserveCalories = false;

        [Section("SETTING_SECTION_ADVANCED", Localize = true)]

        [Name("SETTING_ADVANCED_LOG_INTERACTIVE", Localize = true)]
        [Description("SETTING_ADVANCED_LOG_INTERACTIVE_DESC", Localize = true)]
        public bool InteractiveLog = false;

        [Name("SETTING_ADVANCED_LOG_LIST_ADD", Localize = true)]
        public bool ADVANCED_listadd = false;

        [Name("SETTING_ADVANCED_LOG_DEFINITION", Localize = true)]
        public bool ADVANCED_LogUnused = false;

        [Name("SETTING_ADVANCED_CREATECONFIG", Localize = true)]
        [Description("SETTING_ADVANCED_CREATECONFIG_DESC", Localize = true)]
        public bool ADVANCED_CreateNewConfig = false;

        [Section("Dev Options")]

        [Name("Copy Files to Dev folder")]
        [Description("You should never use this as its only for dev environment")]
        public bool CopyFilesToDev = false;

        protected override void OnConfirm()
        {
            base.OnConfirm();

            Main.LogInteractiveObjectDetails = Instance.InteractiveLog;
        }

        // This is used to load the settings
        internal static void OnLoad()
        {
            Instance.AddToModSettings(BuildInfo.GUIName);
            Instance.RefreshGUI();
        }
    }
}