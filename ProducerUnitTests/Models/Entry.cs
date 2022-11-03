namespace ProducerUnitTests.Models;
public struct Entry
{
    public Entry(string message)
    {
        Message = message;
    }
    public string Message { get; private set; }
}