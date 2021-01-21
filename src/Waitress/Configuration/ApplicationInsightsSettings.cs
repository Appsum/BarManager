namespace Waitress.Configuration
{
    public class ApplicationInsightsSettings
    {
        public static string ConfigurationKey = "Azure:ApplicationInsights";
        public string InstrumentationKey { get; set; }
    }
}