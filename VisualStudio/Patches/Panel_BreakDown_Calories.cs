using FasterHarvesting.Utilities;

namespace FasterHarvesting
{
    [HarmonyPatch(typeof(Panel_BreakDown), nameof(Panel_BreakDown.UpdateEstimatedCaloriesBurnedLabel))]
    public class Panel_BreakDown_Calories
    {
        public static bool Prefix(Panel_BreakDown __instance)
        {
            Il2Cpp.BreakDown breakDown = __instance.m_BreakDown;
            string name = CommonUtils.NormalizeName(breakDown.gameObject.name);

            if (Main.ObjectExists(name))
            {
                if (Settings.Instance.GENERAL_PreserveCalories)
                {
                    float f = GameManager.GetPlayerManagerComponent().CalculateModifiedCalorieBurnRate(GameManager.GetHungerComponent().m_CalorieBurnPerHourBreakingDown) * Main.ObjectsToAlter.Find(d => d.ObjectName == name).ObjectBreakDownTimeOriginal;

                    int g = Mathf.RoundToInt(f);

                    __instance.m_EstimatedCaloriesBurnedLabel.text = $"{g} {Localization.Get("GAMEPLAY_Calories")}";

                    return false;
                }
            }

            return true;
        }
    }
}
