using System.Text;
using System.Text.Json.Serialization;

namespace FasterHarvesting.CustomList
{
    public class CustomToolList : IEquatable<CustomToolList?>
    {
        [JsonInclude]
        public GameObject[] CustomList { get; set; }

        public CustomToolList(GameObject[] CustomList)
        {
            this.CustomList = CustomList;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (GameObject go in CustomList)
            {
                stringBuilder.Append(go.name);
            }

            return stringBuilder.ToString();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as CustomToolList);
        }

        public bool Equals(CustomToolList? other)
        {
            return other is not null &&
                   EqualityComparer<GameObject[]>.Default.Equals(CustomList, other.CustomList);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CustomList);
        }

        public static bool operator ==(CustomToolList? left, CustomToolList? right)
        {
            return EqualityComparer<CustomToolList>.Default.Equals(left, right);
        }

        public static bool operator !=(CustomToolList? left, CustomToolList? right)
        {
            return !(left == right);
        }
    }
}
