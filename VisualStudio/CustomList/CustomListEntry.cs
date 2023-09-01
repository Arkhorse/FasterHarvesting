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

        /// <summary>
        /// Creates a new instance of this class
        /// </summary>
        /// <param name="ObjectName">The exact name, case sensitive, of the prefab you want to target. Without the <c>.prefab</c> extension</param>
        /// <param name="ObjectBreakDownTime">The amount of time, in hours, for how long the break down is done. (90 minutes is <c>1.5d</c>)</param>
        public ICustomListEntry(string ObjectName, double ObjectBreakDownTime)
        {
            this.ObjectName = ObjectName;
            this.ObjectBreakDownTime = ObjectBreakDownTime;
        }

        public override string ToString()
        {
            return $"ObjectName {this.ObjectName}, ObjectBreakDownTime: {this.ObjectBreakDownTime}";
        }
    }
}
