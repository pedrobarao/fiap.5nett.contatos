using MassTransit;
using Event = Commons.Domain.Messages.Event;

namespace MessageBus;

public class MessageBus(/*IBus bus*/) : IMessageBus
{
    public async Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : Event
    {
        throw new NotImplementedException();//await bus.Publish(message, cancellationToken);
    }
}