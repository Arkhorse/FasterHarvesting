using FasterHarvesting.CustomList;
using FasterHarvesting.Utilities;
using System.Linq;
using System.Text;

namespace FasterHarvesting
{
    [HarmonyPatch(typeof(Panel_BreakDown), nameof(Panel_BreakDown.Enable), new Type[] { typeof(bool) })]
    public class Panel_BreakDown_Enable
    {
        public static void Postfix(Panel_BreakDown __instance, ref bool enable)
        {
            if (!enable) return;

            Il2Cpp.BreakDown breakDown = __instance.m_BreakDown;
            string name = CommonUtils.NormalizeName(breakDown.gameObject.name);

            if (Main.ObjectExists(name))
            {
                ICustomListEntry? entry = Main.ObjectsToAlter.Find(d => d.ObjectName == name);

                if (entry is null) return; // should never happen
                if (entry.m_CustomDuration == null)
                {
                    breakDown.m_TimeCostHours = (float)entry.ObjectBreakDownTime;
                }
                else
                {
                    breakDown.m_TimeCostHours = entry.m_CustomDuration.GetDurationSeconds();
                }
            }

            if (Main.LogInteractiveObjectDetails || !Main.ObjectExists(name))
            {
                Logging.LogSeperator();
                Logging.Log("Current interactive object is not contained in any of the configs.");
                Logging.Log($"Interactive object info:");
                Logging.Log($"\tOriginal Name        : {breakDown.gameObject.name}");
                Logging.Log($"\tTrimmed Name         : {name}");
                Logging.Log($"\tBreakdown Time       : {breakDown.m_TimeCostHours}");
                
                if (breakDown.m_UsableTools.Count > 0)
                {
                    Logging.Log("\tList of usable tools:");
                    for (int i = 0; i < breakDown.m_UsableTools.Count; i++)
                    {
                        Logging.Log($"\t\t{breakDown.m_UsableTools[i].name}");
                    }
                }

                Logging.LogSeperator();
            }

            if (Settings.Instance.ADVANCED_CreateNewConfig)
            {
                float time = 0.01f;

                if (breakDown.m_UsableTools.Count == 1 && breakDown.m_UsableTools.Contains(CommonUtils.GetGearItemPrefab("GEAR_Hacksaw").gameObject))
                {
                    time = 0.02f;
                }
                
                if (breakDown.m_UsableTools.Count > 0)
                {
                    List<string> tools = new();

                    for (int i = 0; i < breakDown.m_UsableTools.Count; i++)
                    {
                        tools.Add(breakDown.m_UsableTools[i].name);
                    }
                    CustomListHandler.CreateNewConfig(name, time, breakDown.m_TimeCostHours, tools);
                }
                else if (breakDown.m_UsableTools.Count == 0)
                {
                    CustomListHandler.CreateNewConfig(name, time, breakDown.m_TimeCostHours);
                }
            }
        }
    }
}
