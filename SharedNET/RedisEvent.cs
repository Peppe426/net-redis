namespace SharedNET;

public record RedisEvent
{
    public RedisEvent(string origion, string message)
    {
        Origion = origion;
        Message = message;
    }

    public string Origion { get; private set; }
    public string Message { get; private set; }
}