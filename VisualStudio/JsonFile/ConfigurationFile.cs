namespace FasterHarvesting.JsonFile
{
    public class ConfigurationFile
    {
        public string ObjectName { get; set; } = string.Empty;
        public string ObjectTag { get; set; } = string.Empty;
        public float ObjectBreadownTimeOriginal { get; set; }
        public float ObjectBreakdownTimeModified { get; set; }
        public List<string> ObjectBreakdownToolsOriginal { get; set; } = new();
        public float ObjectBreakdownCaloriesOriginal { get; set; }
    }
}
