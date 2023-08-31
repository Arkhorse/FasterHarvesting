namespace FasterHarvesting
{
    [HarmonyPatch(typeof(Panel_BreakDown), nameof(Panel_BreakDown.Enable), new Type[] { typeof(bool) })]
    public class Panel_BreakDown_Enable
    {
        public static void Postfix(Panel_BreakDown __instance, ref bool enable)
        {
            if (!enable) return;

            Logger.Log("Panel_BreakDown");

            if (__instance.m_BreakDown.gameObject.name == "INTERACTIVE_LimbA_Prefab")
            {
                Logger.Log($"Limb found");
                __instance.m_BreakDown.m_TimeCostHours = (float)Settings.Instance.limbATime;
            }
            else if (__instance.m_BreakDown.gameObject.name == "INTERACTIVE_LimbB_Prefab")
            {
                Logger.Log($"Limb found");
                __instance.m_BreakDown.m_TimeCostHours = (float)Settings.Instance.limbBTime;
            }
            else if (__instance.m_BreakDown.gameObject.name == "INTERACTIVE_LimbA_Big_Prefab")
            {
                Logger.Log($"Limb found");
                __instance.m_BreakDown.m_TimeCostHours = (float)Settings.Instance.limbABigTime;
            }
            else if (__instance.m_BreakDown.gameObject.name == "INTERACTIVE_LimbB_Big_Prefab")
            {
                Logger.Log($"Limb found");
                __instance.m_BreakDown.m_TimeCostHours = (float)Settings.Instance.limbBBigTime;
            }
            else if (__instance.m_BreakDown.gameObject.name == "INTERACTIVE_BranchA_Prefab")
            {
                Logger.Log($"Limb found");
                __instance.m_BreakDown.m_TimeCostHours = (float)Settings.Instance.branchATime;
            }
        }
    }
}
