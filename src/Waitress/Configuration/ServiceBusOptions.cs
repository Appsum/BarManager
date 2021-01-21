namespace Waitress.Configuration
{
    public class ServiceBusSettings
    {
        public static string ConfigurationKey = "Azure:ServiceBus";
        public string ConnectionString { get; set; }
        public string TopicName { get; set; }
    }
}