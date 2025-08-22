namespace EMC.BuildingBlocks.Static.Extensions
{
    public static class ConfigurationExtensions
    {

        public static Dictionary<string, string> FilterMailSettings(this Dictionary<string, string> config)
        {
            return config
                .Where(kvp => kvp.Key.StartsWith("Mail:", StringComparison.OrdinalIgnoreCase))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }


    }
}
