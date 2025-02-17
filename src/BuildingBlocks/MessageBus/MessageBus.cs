using MassTransit;
using Event = Commons.Domain.Messages.Event;

namespace MessageBus;

public class MessageBus(IBus bus) : IMessageBus
{
    public async Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : Event
    {
        await bus.Publish(message, cancellationToken);
    }
}