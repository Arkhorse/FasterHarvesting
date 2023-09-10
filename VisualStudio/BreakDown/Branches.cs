namespace FasterHarvesting.BreakDown
{
    internal class Branches : IBreakDown
    {
        public static void HarvestAlter(Panel_BreakDown breakDown, string NormalizedName)
        {
            if (Main.DefaultObjects.Exists(d => d.ObjectName == NormalizedName))
            {
                breakDown.m_BreakDown.m_TimeCostHours = (float)Main.DefaultObjects.Find(d => d.ObjectName == NormalizedName)!.ObjectBreakDownTime;
            }
        }
    }
}
