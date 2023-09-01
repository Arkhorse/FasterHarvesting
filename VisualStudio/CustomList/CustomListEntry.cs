using System.Text.Json.Serialization;

namespace FasterHarvesting.CustomList
{
    internal class ICustomListEntry
    {
#nullable disable
        [JsonInclude]
        public virtual string ObjectName { get; set; }
        [JsonInclude]
        public virtual double ObjectBreakDownTime { get; set; }
#nullable enable

        public ICustomListEntry(string ObjectName, double ObjectBreakDownTime)
        {
            this.ObjectName = ObjectName;
            this.ObjectBreakDownTime = ObjectBreakDownTime;
        }
    }
}
