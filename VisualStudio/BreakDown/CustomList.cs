//namespace FasterHarvesting.BreakDown
//{
//    public class CustomList : IBreakDown
//    {
//		public static void HarvestAlter(Panel_BreakDown breakDown, string NormalizedName)
//        {
//            if (Settings.Instance.AllowCustomList && Main.entries.Count > 0)
//            {
//                Logging.Log("Custom List");
//                // NormalizeName is needed here as (Clone) is extremely common
//                if (Main.entries.Exists(f => f.ObjectName == NormalizedName))
//                {
//                    Logging.Log($"Object with name {NormalizedName} exists in the list");
//                    breakDown.m_BreakDown.m_TimeCostHours = (float)Main.entries.Find(f => f.ObjectName == NormalizedName)!.ObjectBreakDownTime;
//                }
//            }
//            else if (Settings.Instance.AllowCustomList && Main.entries.Count == 0)
//            {
//                Logging.LogWarning("entries.Count is 0 while the setting is enabled");
//            }
//        }
//    }
//}
