namespace Bartender.Configuration
{
    public class TableStorageSettings
    {
        public static string ConfigurationKey = "Azure:TableStorage";
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
    }
}