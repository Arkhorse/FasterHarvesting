namespace FasterHarvesting
{
    public class Settings : JsonModSettings
    {
        internal static Settings Instance { get; } = new();

        [Name("Cedar Limb")]
        [Slider(0.01f, 12f)]
        public double limbATime = 1.5f;

        [Name("Fir Limb")]
        [Slider(0.01f, 12f)]
        public double limbBTime = 1.5f;

        [Name("Big Cedar Limb")]
        [Slider(0.01f, 12f)]
        public double limbABigTime = 1.5f;

        [Name("Big Fir Limb")]
        [Slider(0.01f, 12f)]
        public double limbBBigTime = 1.5f;

        [Name("Branch")]
        [Slider(0.01f, 12f)]
        public double branchATime = 0.170f;

        // This is used to load the settings
        internal static void OnLoad()
        {
            Instance.AddToModSettings(BuildInfo.GUIName);
            Instance.RefreshGUI();
        }
    }
}