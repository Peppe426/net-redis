namespace Redis.Stream.Producer;

public class RedisConfiguration
{
    public RedisConfiguration(string connectionString, int port, string key, string streamField)
    {
        ConnectionString = connectionString;
        Port = port;
        Key = key;
        StreamField = streamField;
    }
    public string ConnectionString { get; private set; }
    public int Port { get; set; }
    public string Key { get; private set; }
    public string StreamField { get; private set; }
}