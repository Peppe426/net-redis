using StackExchange.Redis;

namespace Redis.Stream.Consumer;

public class RedisHandler : IRedisHandler
{
    public RedisHandler(ConsumerConfiguration configuration)
    {
        _configuration = configuration;
    }

    private ConsumerConfiguration _configuration;
    private bool _group = false;

    private ConnectionMultiplexer? _redisConnection { get; set; } = default;

    public async Task<(bool isSuccess, ConnectionMultiplexer connection)> Connect()
    {
        try
        {
            if (_redisConnection is null)
            {
                _redisConnection = await ConnectionMultiplexer.ConnectAsync(_configuration.ConnectionString);
            }

            return (true, _redisConnection!);
        }
        catch (Exception ex)
        {
            throw new NullReferenceException(ex.Message, ex);
        }
    }
    private async Task<bool> ConsumerGroupExist()
    {
        var db = _redisConnection!.GetDatabase();
        StreamGroupInfo[] groups = await db.StreamGroupInfoAsync(_configuration.Key);
        bool consumerGroupFound = false;
        for (int i = 0; i < groups.Length; i++)
        {
            if (groups[i].Name == _configuration.ConsumerGroup)
            {
                consumerGroupFound = true;
            }
        }

        return consumerGroupFound;
    }

    public async Task ConsumeMessages()
    {
        if (_redisConnection?.IsConnected is false)
            await Connect();

        var db = _redisConnection!.GetDatabase();

        var groupInfo = await db.StreamGroupInfoAsync(_configuration.Key);

        if (await ConsumerGroupExist() is false)
        {
            await CreateConsumerGroup();
        }

        //db.StreamDeleteConsumerGroup(_configuration.Key, _configuration.ConsumerGroup);
        //db.StreamDeleteConsumerGroup(_configuration.Key, _configuration.ConsumerGroup+"2");

        //var test1 = await db.StreamReadAsync(_configuration.Key, StreamPosition.NewMessages);

        //https://stackexchange.github.io/StackExchange.Redis/Streams

        //db.StreamDeleteConsumerGroup(_configuration.Key, _configuration.ConsumerGroup);

        //var streamEntry1 = await db.StreamReadGroupAsync(_configuration.Key, _configuration.ConsumerGroup, "consumer_2", ">", count: 5);
        var streamEntry1 = await db.StreamReadGroupAsync(_configuration.Key, _configuration.ConsumerGroup, "consumer_1", ">", count: 1); //TODO fixa till du har koll :)

        foreach (var entry in streamEntry1)
        {
            Console.WriteLine(entry.Id);
            Console.WriteLine(entry.Values.FirstOrDefault().Value.ToString());
            //Console.WriteLine(JsonSerializer.Serialize(entry.Values.FirstOrDefault().Value.ToString());
        }

        Console.WriteLine("---------------------------------------------------");

        //var pendingInfo = db.StreamPending(_configuration.Key, _configuration.ConsumerGroup);

        //db.StreamAcknowledgeAsync(_configuration.Key, _configuration.ConsumerGroup, "1660842901666-0");

        //Console.WriteLine(pendingInfo.PendingMessageCount);
        //Console.WriteLine(pendingInfo.LowestPendingMessageId);
        //Console.WriteLine(pendingInfo.HighestPendingMessageId);
        //Console.WriteLine($"Consumer count: {pendingInfo.Consumers.Length}.");
        ////Console.WriteLine(pendingInfo.Consumers.First().Name);
        ////Console.WriteLine(pendingInfo.Consumers.First().PendingMessageCount);
        //foreach (var c in pendingInfo.Consumers)
        //{
        //    Console.WriteLine($"---------------------------------------------------------------");

        //    Console.WriteLine($"Consumer name: {c.Name}");
        //    Console.WriteLine($"PendingMessageCount: {c.PendingMessageCount}");

        //    var pendingMessages = db.StreamPendingMessages(_configuration.Key, _configuration.ConsumerGroup, count: c.PendingMessageCount, consumerName: c.Name, minId: pendingInfo.LowestPendingMessageId);

        //    foreach (var pm in pendingMessages)
        //    {
        //        Console.WriteLine($"----------------------{pm.MessageId}-----------------------------------------");

        //        Console.WriteLine($"MessageId: {pm.MessageId}");
        //        Console.WriteLine($"ConsumerName: {pm.ConsumerName}");
        //        Console.WriteLine($"DeliveryCount: {pm.DeliveryCount}");
        //        Console.WriteLine($"IdleTimeInMilliseconds: {pm.IdleTimeInMilliseconds}");

        //    }
        //}

        //var pendingMessages = db.StreamPendingMessages(_configuration.Key, _configuration.ConsumerGroup, count: 1, consumerName: "consumer_1", minId: pendingInfo.LowestPendingMessageId);
    }

    public async Task<(bool isSuccess, string consumerGroupeName)> CreateConsumerGroup()
    {
        var db = _redisConnection!.GetDatabase();

        _group = await db.StreamCreateConsumerGroupAsync(_configuration.Key, _configuration.ConsumerGroup, StreamPosition.NewMessages);
        return (_group, _configuration.ConsumerGroup);
    }
}