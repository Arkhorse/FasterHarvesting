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

            if (Main.DefaultObjects.Exists(d => d.ObjectName == NormalizedName))
            {
                __instance.m_BreakDown.m_TimeCostHours = (float)Main.DefaultObjects.Find(d => d.ObjectName == NormalizedName)!.ObjectBreakDownTime;
            }

            #region CustomList

            if (Settings.Instance.AllowCustomList && Main.entries.Count > 0)
            {
                // NormalizeName is needed here as (Clone) is extremely common
                if (Main.entries.Exists(f => f.ObjectName == NormalizedName))
                {
                    __instance.m_BreakDown.m_TimeCostHours = (float)Main.entries.Find(f => f.ObjectName == NormalizedName)!.ObjectBreakDownTime;
                }
                else if (Main.LogInteractiveObjectDetails)
                {
                    Logger.LogSeperator();
                    Logger.Log("[INFO] Currently interactive object is not contained in any of the custom configs.");
                    Logger.Log($"Interactive object info:\nName: {NormalizedName}\n Breakdown Time: {__instance.m_BreakDown.m_TimeCostHours}");
                    Logger.LogSeperator();
                }
            }
            else if (Settings.Instance.AllowCustomList && Main.entries.Count == 0)
            {
                Logger.LogWarning("entries.Count is 0 while the setting is enabled");
            }

            #endregion
        }
    }
}
