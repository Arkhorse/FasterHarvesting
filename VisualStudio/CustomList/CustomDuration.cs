

using System.Text;

namespace FasterHarvesting.CustomList
{
    public class CustomDuration : IEquatable<CustomDuration?>
    {
        int Days { get; set; }
        int Hours { get; set; }
        int Minutes { get; set; }
        int Seconds { get; set; }
        public bool UseCustomDuration { get; set; }

        public int GetDurationSeconds()
        {
            return ConvertDaysToSeconds(Days) + ConvertHoursToSeconds(Hours) + ConvertMinutesToSeconds(Minutes) + Seconds;
        }

        int ConvertDaysToSeconds(int day)
        {
            return day * 86400;
        }

        int ConvertHoursToSeconds(int hour)
        {
            return hour * 3600;
        }

        int ConvertMinutesToSeconds(int minute)
        {
            return minute * 60;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as CustomDuration);
        }

        public bool Equals(CustomDuration? other)
        {
            return other is not null &&
                   Days == other.Days &&
                   Hours == other.Hours &&
                   Minutes == other.Minutes &&
                   Seconds == other.Seconds &&
                   UseCustomDuration == other.UseCustomDuration;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Days, Hours, Minutes, Seconds, UseCustomDuration);
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append($"Custom Duration: D:{Days}H:{Hours}M:{Minutes}S:{Seconds}");

            return sb.ToString();
        }

        public CustomDuration() { }
        public CustomDuration(int days, int hours, int minutes, int seconds, bool useCustomDuration)
        {
            Days                = days;
            Hours               = hours;
            Minutes             = minutes;
            Seconds             = seconds;
            UseCustomDuration   = useCustomDuration;
        }

        public static bool operator ==(CustomDuration? left, CustomDuration? right)
        {
            return EqualityComparer<CustomDuration>.Default.Equals(left, right);
        }

        public static bool operator !=(CustomDuration? left, CustomDuration? right)
        {
            return !(left == right);
        }
    }
}
