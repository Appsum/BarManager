namespace Waitress.Configuration
{
    public class BlobStorageSettings
    {
        public static string ConfigurationKey = "Azure:BlobStorage";
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
    }
}