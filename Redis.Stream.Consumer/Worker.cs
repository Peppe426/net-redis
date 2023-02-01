using StackExchange.Redis;

namespace Redis.Stream.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private RedisHandler _redisHandler;
        private ConnectionMultiplexer? _redisConnection;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            initConsumerAsync();
        }

        private void initConsumerAsync()
        {
            var configuration = new ConsumerConfiguration("127.0.0.1:6379", "test", "message", "axakon2");
            _redisHandler = new RedisHandler(configuration);
            _redisConnection = _redisHandler?.Connect().Result.connection;

            //var redisEvent = new RedisEvent("WeatherApp", "Sun and clear");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _redisHandler.ConsumeMessages();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}