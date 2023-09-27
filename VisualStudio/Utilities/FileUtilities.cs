using System.Runtime.InteropServices;
using FasterHarvesting.Utilities.Enums;
// https://github.com/Willster419/RelhaxModpack/blob/master/RelhaxModpack/RelhaxModpack/Utilities/FileUtils.cs

namespace FasterHarvesting.Utilities
{
    public class FileUtilities
    {
        /// <summary>
        /// Return a list of files from a directory, including their paths.
        /// </summary>
        /// <param name="directoryPath">The directory to search for files</param>
        /// <param name="option">Specifies to search this top directory or subdirectories to the Directory.GetFiles() method</param>
        /// <param name="filesOnly">Toggle if the returned list should be pre-filtered for only have files (no directories)</param>
        /// <param name="searchPattern">The search pattern for finding files in a directory</param>
        /// <returns>The list of files if the search operation was successful, otherwise null</returns>
        public static string[]? FileSearch(string directoryPath, SearchOption option, bool filesOnly, string searchPattern = "*.json")
        {
            try
            {
                //verify the folder to search exists
                if (!Directory.Exists(directoryPath))
                {
                    Logging.LogWarning($"Path {directoryPath} does not exist");
                    return null;
                }

                //do the actual file search
                List<string> files = Directory.GetFiles(directoryPath, searchPattern, option).ToList();

                //if requested, filter out any folders
                if (filesOnly)
                    files = files.Where(item => File.Exists(item)).ToList();

                return files.ToArray();
            }
            catch (Exception e)
            {
                Logging.LogError($"Failed to get files for directory {Path.GetFullPath(directoryPath)}\n{e.ToString()}");
                return null;
            }
        }

        #region Special Folder Stuff

        //https://stackoverflow.com/a/21953690/3128017
        private static string[] _knownFolderGuids = new string[]
        {
            "{56784854-C6CB-462B-8169-88E350ACB882}", // Contacts
            "{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}", // Desktop
            "{FDD39AD0-238F-46AF-ADB4-6C85480369C7}", // Documents
            "{374DE290-123F-4565-9164-39C4925E467B}", // Downloads
            "{1777F761-68AD-4D8A-87BD-30B759FA33DD}", // Favorites
            "{BFB9D5E0-C6A9-404C-B2B2-AE6DB6AF4968}", // Links
            "{4BD8D571-6D19-48D3-BE97-422220080E43}", // Music
            "{33E28130-4E1E-4676-835A-98395C3BC3BB}", // Pictures
            "{4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4}", // SavedGames
            "{7D1D3A04-DEBB-4115-95CF-2F29DA2920DA}", // SavedSearches
            "{18989B1D-99B5-455B-841C-AB7C74E4DDFC}", // Videos
        };
        /// <summary>
        /// Gets the current path to the specified known folder as currently configured. This does
        /// not require the folder to be existent.
        /// </summary>
        /// <param name="knownFolder">The known folder which current path will be returned.</param>
        /// <returns>The default path of the known folder.</returns>
        /// <exception cref="System.Runtime.InteropServices.ExternalException">Thrown if the path
        ///     could not be retrieved.</exception>
        public static string? GetSpecialFolderPath(KnownFolder knownFolder)
        {
            return GetSpecialFolderPath(knownFolder, false);
        }

        /// <summary>
        /// Gets the current path to the specified known folder as currently configured. This does
        /// not require the folder to be existent.
        /// </summary>
        /// <param name="knownFolder">The known folder which current path will be returned.</param>
        /// <param name="defaultUser">Specifies if the paths of the default user (user profile
        ///     template) will be used. This requires administrative rights.</param>
        /// <returns>The default path of the known folder.</returns>
        /// <exception cref="System.Runtime.InteropServices.ExternalException">Thrown if the path
        ///     could not be retrieved.</exception>
        public static string? GetSpecialFolderPath(KnownFolder knownFolder, bool defaultUser)
        {
            return GetSpecialFolderPath(knownFolder, KnownFolderFlags.DontVerify, defaultUser);
        }

        private static string? GetSpecialFolderPath(KnownFolder knownFolder, KnownFolderFlags flags, bool defaultUser)
        {
            int result = SHGetKnownFolderPath(new Guid(_knownFolderGuids[(int)knownFolder]), (uint)flags, new IntPtr(defaultUser ? -1 : 0), out IntPtr outPath);
            if (result >= 0)
            {
                string? path = Marshal.PtrToStringUni(outPath);
                Marshal.FreeCoTaskMem(outPath);
                return path;
            }
            else
            {
                throw new ExternalException("Unable to retrieve the known folder path. It may not be available on this system.", result);
            }
        }

        [DllImport("Shell32.dll")]
        private static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr ppszPath);
        #endregion
    }
}
