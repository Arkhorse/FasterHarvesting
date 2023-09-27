namespace FasterHarvesting.Patches
{
    [HarmonyPatch(typeof(Panel_BreakDown), nameof(Panel_BreakDown.BreakDownFinished))]
    public class Panel_BreakDown_BreakDownFinished
    {
        public static void Postfix()
        {
            if (Settings.Instance.GENERAL_PreserveCalories)
            {
                GameManager.GetHungerComponent().RemoveReserveCalories(Main.Config.BreakDownCalories);
            }
        }
    }
}
