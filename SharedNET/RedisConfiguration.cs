namespace SharedNET;

public record RedisConfiguration
{
    public RedisConfiguration(string connectionString, string key, string streamField)
    {
        ConnectionString = connectionString;
        Key = key;
        StreamField = streamField;
    }

    public string ConnectionString { get; private set; }
    public string Key { get; private set; }
    public string StreamField { get; private set; }
}