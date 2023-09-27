using System.Runtime.CompilerServices;
using System.Diagnostics;
using Il2CppSystem.Text.RegularExpressions;

namespace FasterHarvesting.Utilities
{
    public static class CommonUtils
    {
        /// <summary>
        /// Multiply by this value to convert milliseconds to seconds
        /// </summary>
        public const int TO_SECONDS = 1000;

        /// <summary>
        /// Multiply by this value to convert seconds to minuets
        /// </summary>
        public const int TO_MINUTES = 60;

        [return: NotNullIfNotNull(nameof(name))]
        public static GearItem GetGearItemPrefab(string name) => GearItem.LoadGearItemPrefab(name);
        private static string pattern = @"(?:\(\d{0,}\))";
        private static string pattern2 = @"(?:\s\d{0,})";

        /// <summary>
        /// Normalizes the name given to remove extra bits using regex for most of the changes
        /// </summary>
        /// <param name="name">The name of the thing to normalize</param>
        /// <returns>Normalized name without <c>(Clone)</c> or any numbers appended</returns>
        /// <remarks>
        /// <para>Regex used: <c>@"(?:\(\d{1,}\))"</c></para>
        /// <para>Things removed: <c>(Clone)</c>, whitespace and appended numbers</para>
        /// </remarks>
        [return: NotNullIfNotNull(nameof(name))]
        public static string? NormalizeName(string name)
        {
            string name0 = Regex.Replace(name, pattern, string.Empty);
            string name1 = Regex.Replace(name0, pattern2, string.Empty);
            string name2 = name1.Replace("(Clone)", string.Empty, StringComparison.InvariantCultureIgnoreCase);
            string name3 = name2.Replace("\0", string.Empty);
            return name3.Trim();
        }

        public static string GetFileNameFromPath(string path)
        {
            return Path.GetFileName(path);
        }

        public static string? GetLOD0Name(string name)
        {
            return $"{NormalizeName(name)}_LOD0";
        }

        public static string? GetPrefabName(string name)
        {
            return $"{NormalizeName(name)}_Prefab";
        }

        public static string? GetShadowName(string name)
        {
            return $"{NormalizeName(name)}_Shadow";
        }

        [return: NotNullIfNotNull(nameof(component))]
        public static T? GetComponentSafe<T>(this Component? component) where T : Component
        {
            return component == null ? default : GetComponentSafe<T>(component.gameObject);
        }

        [return: NotNullIfNotNull(nameof(gameObject))]
        public static T? GetComponentSafe<T>(this GameObject? gameObject) where T : Component
        {
            return gameObject == null ? default : gameObject.GetComponent<T>();
        }

        public static T GetItem<T>(string name, string? reference = null) where T : Component
        {
            GameObject? gameObject = AssetBundleUtils.LoadAsset<GameObject>(name);
            if (gameObject == null)
            {
                throw new ArgumentException("Could not load '" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + ".");
            }

            T targetType = GetComponentSafe<T>(gameObject);
            if (targetType == null)
            {
                throw new ArgumentException("'" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + " is not a '" + typeof(T).Name + "'.");
            }

            return targetType;
        }

        /// <summary>
        /// Gets the name of the method above this
        /// </summary>
        /// <returns>The name of the calling method on this method</returns>
        /// <remarks>This is mostly used for in logging, to log the name of the method
        /// See https://stackoverflow.com/a/2652481/3128017 </remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetExecutingMethodName()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(2);
            return sf.GetMethod().Name;
        }

        /// <summary>
        /// Gets the name of the class above this
        /// </summary>
        /// <returns>The name of the calling class on this method call</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetExecutingClassName()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(2);
            return sf.GetMethod().DeclaringType.Name;
        }
    }
}
