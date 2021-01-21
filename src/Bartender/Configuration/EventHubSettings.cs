namespace Bartender.Configuration
{
    public class EventHubSettings
    {
        public static string ConfigurationKey = "Azure:EventHub";
        public string ConnectionString { get; set; }
        public string HubName { get; set; }
    }
}