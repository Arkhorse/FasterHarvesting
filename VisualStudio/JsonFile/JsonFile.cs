using System.Text.Json;

using FasterHarvesting.Utilities;

namespace FasterHarvesting.JsonFile
{
    public sealed class JsonFile
    {
        public static JsonSerializerOptions DefaultOptions { get; } = new JsonSerializerOptions() { WriteIndented = true, IncludeFields = true };

        /// <summary>
        /// Loads a given JSON file
        /// </summary>
        /// <typeparam name="T">The class to deserialize</typeparam>
        /// <param name="configFileName">absolute path to the file</param>
        /// <returns>new class based on file contents</returns>
        public static async Task<T> Load<T>(string configFileName, JsonSerializerOptions? options = null)
        {
            try
            {
                options ??= DefaultOptions;
                await using FileStream file = File.Open(configFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                var output = await JsonSerializer.DeserializeAsync<T>(file, options);
                await file.DisposeAsync();
                return output;
            }
            catch
            {
                Logging.LogError($"Attempting to load the config file failed, file: {configFileName}");
                throw;
            }
        }

        /// <summary>
        /// Saves a new JSON file
        /// </summary>
        /// <typeparam name="T">The class to serialize</typeparam>
        /// <param name="configFileName">absolute path to the file</param>
        /// <param name="Tinput">an instance of the given class with information filled</param>
        public static async Task Save<T>(string configFileName, T Tinput, JsonSerializerOptions? options = null)
        {
            try
            {
                options ??= DefaultOptions;
                await using FileStream file = File.Open(configFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                await JsonSerializer.SerializeAsync<T>(file, Tinput, options);
                await file.DisposeAsync();
            }
            catch
            {
                Logging.LogError($"Attempting to save failed");
                throw;
            }
        }
    }
}
