namespace FasterHarvesting
{
    public class Settings : JsonModSettings
    {
        internal static Settings Instance { get; } = new();

        [Name("GAMEPLAY_BranchBigSoft", Localize = true)]
        [Slider(0.01f, 12.00f, NumberFormat = "{0:F2}")]
        public double limbATime = 1.50f;

        [Name("GAMEPLAY_BranchBigHard", Localize = true)]
        [Slider(0.01f, 12.00f, NumberFormat = "{0:F2}")]
        public double limbBTime = 1.50f;

        [Name("GAMEPLAY_BranchBigSoft", Localize = true)]
        [Slider(0.01f, 12.00f, NumberFormat = "{0:F2}")]
        public double limbABigTime = 1.50f;

        [Name("GAMEPLAY_BranchBigHard", Localize = true)]
        [Slider(0.01f, 12.00f, NumberFormat = "{0:F2}")]
        public double limbBBigTime = 1.50f;

        [Name("GAMEPLAY_branch", Localize = true)]
        [Slider(0.01f, 12.00f, NumberFormat = "{0:F2}")]
        public double branchATime = 0.170f;

        // This is used to load the settings
        internal static void OnLoad()
        {
            Instance.AddToModSettings(BuildInfo.GUIName);
            Instance.RefreshGUI();
        }
    }
}