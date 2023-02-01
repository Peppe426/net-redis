using SharedNET;

namespace Redis.Stream.Consumer;

public record ConsumerConfiguration : RedisConfiguration
{
    public ConsumerConfiguration(string connectionString, string streamName, string streamField, string consumerGroup) : base(connectionString, streamName, streamField)
    {
        ConsumerGroup = consumerGroup;
    }

    public string ConsumerGroup { get; private set; }
}