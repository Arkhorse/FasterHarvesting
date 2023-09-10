namespace FasterHarvesting
{
    [HarmonyPatch(typeof(Panel_BreakDown), nameof(Panel_BreakDown.Enable), new Type[] { typeof(bool) })]
    public class Panel_BreakDown_Enable
    {
        public static void Postfix(Panel_BreakDown __instance, ref bool enable)
        {
            if (!enable) return;

            //Logger.Log("Panel_BreakDown");
            string NormalizedName = Utilities.NormalizeName(__instance.m_BreakDown.gameObject.name);

            BreakDown.Branches.HarvestAlter(__instance, NormalizedName);
            BreakDown.CustomList.HarvestAlter(__instance, NormalizedName);

            if (Main.LogInteractiveObjectDetails)
            {
                Logger.LogSeperator();
                Logger.Log("[INFO] Currently interactive object is not contained in any of the custom configs.");
                Logger.Log($"Interactive object info:\nName: {NormalizedName}\n Breakdown Time: {__instance.m_BreakDown.m_TimeCostHours}");
                Logger.LogSeperator();
            }
        }
    }
}
