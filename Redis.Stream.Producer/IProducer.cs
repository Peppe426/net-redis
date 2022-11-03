namespace Redis.Stream.Producer;

public interface IProducer<T> where T : struct
{
    Task<(bool isSuccess, string response)> EmitEntryToStream(T entry, CommandFlags commandFlags = CommandFlags.None);
}
