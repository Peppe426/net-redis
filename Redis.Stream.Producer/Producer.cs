using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Redis.Stream.Producer;

public class Producer<T> : IProducer<T> where T : struct
{
    private RedisConfiguration _configuration;
    private ConnectionMultiplexer _redisConnection { get; set; }

    public Producer(string connectionString, int port, string key, string streamField)
    {
        _configuration = new RedisConfiguration(connectionString, port, key, streamField);
    }
    private async Task<(bool isSuccess, ConnectionMultiplexer? connection)> Connect()
    {
        var options = new ConfigurationOptions
        {
            AbortOnConnectFail = false,
            IncludeDetailInExceptions = true,
            IncludePerformanceCountersInExceptions = true,
            EndPoints = { { _configuration.ConnectionString, _configuration.Port } },
            //ConnectRetry = 3,
            //DefaultDatabase = 0,
            //Ssl = true,     
            //Password = "secretPassword"
        };
        try
        {
            _redisConnection = await ConnectionMultiplexer.ConnectAsync(options);
            return (_redisConnection.IsConnected, _redisConnection);
        }
        catch (Exception ex)
        {
            throw new NullReferenceException(ex.Message, ex);
        }
    }

    public async Task<(bool isSuccess, string response)> EmitEntryToStream(T entry, CommandFlags commandFlags = CommandFlags.None)
    {
        if (_redisConnection is null)
        {
            var connectionResult = await Connect();
            if (connectionResult.isSuccess is false)
            {
                throw new Exception("Could not connect to redis");
            }
        }
        var db = _redisConnection!.GetDatabase();
        string streamEntry = JsonSerializer.Serialize(entry);

        RedisValue messageId = await db.StreamAddAsync(_configuration.Key, _configuration.StreamField, streamEntry, null, null, false, commandFlags);

        if (messageId.IsNullOrEmpty)
        {
            throw new RedisException("Failed to emit message to a Redis Stream. Return message was either null or empty.");
        }
        return (true, messageId!);
    }
}




//https://github.com/StackExchange/StackExchange.Redis/issues/883
