using FluentAssertions;
using ProducerUnitTests.Models;
using Redis.Stream.Producer;

namespace ProducerUnitTests;
public class ProducerTests
{
    [Test]
    public async Task ShouldEmitEntryToRedisStream()
    {
        //Given
        Producer<Entry> producer = new Producer<Entry>("127.0.0.1", 1337, "test", "message");
        Entry entry = new Entry("This is a test message");
        //When
        var output = await producer.EmitEntryToStream(entry);
        //Then
        output.isSuccess.Should().BeTrue();
    }
    [Test]
    public async Task ShouldThrowExceptionWhenEmitEntryToRedisStream()
    {
        //Given
        Producer<Entry> producer = new Producer<Entry>("0.0.0.1:6379", 0000, "test", "message");
        Entry entry = new Entry("This is a test message, should not work!");
        //When
        Func<Task> action = async () => await producer.EmitEntryToStream(entry);
        //Then
        await action.Should().ThrowAsync<Exception>();
    }
}