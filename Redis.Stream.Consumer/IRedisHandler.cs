using StackExchange.Redis;
namespace Redis.Stream.Consumer;
public interface IRedisHandler
{
    Task<(bool isSuccess, ConnectionMultiplexer connection)> Connect();
    Task<(bool isSuccess, string consumerGroupeName)> CreateConsumerGroup();
    Task ConsumeMessages();
}

