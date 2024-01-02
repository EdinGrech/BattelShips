namespace BattelShipCore
{
    public class ConfigurationSettings(string connectionString)
    {
        public string ConnectionString { get; set; } = connectionString;
    }
}