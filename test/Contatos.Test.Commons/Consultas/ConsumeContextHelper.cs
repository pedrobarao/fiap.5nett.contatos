using System.Diagnostics.CodeAnalysis;
using MassTransit;

namespace Contatos.Test.Commons.Consultas;

public abstract class ConsumeContextHelper<T> : ConsumeContext<T> where T : class
{
    public bool HasPayloadType(Type payloadType)
    {
        throw new NotImplementedException();
    }

    public bool TryGetPayload<T1>([NotNullWhen(true)] out T1? payload) where T1 : class
    {
        throw new NotImplementedException();
    }

    public T1 GetOrAddPayload<T1>(PayloadFactory<T1> payloadFactory) where T1 : class
    {
        throw new NotImplementedException();
    }

    public T1 AddOrUpdatePayload<T1>(PayloadFactory<T1> addFactory, UpdatePayloadFactory<T1> updateFactory) where T1 : class
    {
        throw new NotImplementedException();
    }

    public CancellationToken CancellationToken { get; }
    public Guid? MessageId { get; }
    public Guid? RequestId { get; }
    public Guid? CorrelationId { get; }
    public Guid? ConversationId { get; }
    public Guid? InitiatorId { get; }
    public DateTime? ExpirationTime { get; }
    public Uri? SourceAddress { get; }
    public Uri? DestinationAddress { get; }
    public Uri? ResponseAddress { get; }
    public Uri? FaultAddress { get; }
    public DateTime? SentTime { get; }
    public Headers Headers { get; }
    public HostInfo Host { get; }
    public ConnectHandle ConnectPublishObserver(IPublishObserver observer)
    {
        throw new NotImplementedException();
    }

    public Task Publish<T1>(T1 message, CancellationToken cancellationToken = new CancellationToken()) where T1 : class
    {
        throw new NotImplementedException();
    }

    public Task Publish<T1>(T1 message, IPipe<PublishContext<T1>> publishPipe, CancellationToken cancellationToken = new CancellationToken()) where T1 : class
    {
        throw new NotImplementedException();
    }

    public Task Publish<T1>(T1 message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = new CancellationToken()) where T1 : class
    {
        throw new NotImplementedException();
    }

    public Task Publish(object message, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task Publish(object message, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task Publish(object message, Type messageType, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task Publish(object message, Type messageType, IPipe<PublishContext> publishPipe,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task Publish<T1>(object values, CancellationToken cancellationToken = new CancellationToken()) where T1 : class
    {
        throw new NotImplementedException();
    }

    public Task Publish<T1>(object values, IPipe<PublishContext<T1>> publishPipe, CancellationToken cancellationToken = new CancellationToken()) where T1 : class
    {
        throw new NotImplementedException();
    }

    public Task Publish<T1>(object values, IPipe<PublishContext> publishPipe, CancellationToken cancellationToken = new CancellationToken()) where T1 : class
    {
        throw new NotImplementedException();
    }

    public ConnectHandle ConnectSendObserver(ISendObserver observer)
    {
        throw new NotImplementedException();
    }

    public Task<ISendEndpoint> GetSendEndpoint(Uri address)
    {
        throw new NotImplementedException();
    }

    public bool HasMessageType(Type messageType)
    {
        throw new NotImplementedException();
    }

    public bool TryGetMessage<T1>([NotNullWhen(true)] out ConsumeContext<T1>? consumeContext) where T1 : class
    {
        throw new NotImplementedException();
    }

    public void AddConsumeTask(Task task)
    {
        throw new NotImplementedException();
    }

    public Task RespondAsync<T1>(T1 message) where T1 : class
    {
        throw new NotImplementedException();
    }

    public Task RespondAsync<T1>(T1 message, IPipe<SendContext<T1>> sendPipe) where T1 : class
    {
        throw new NotImplementedException();
    }

    public Task RespondAsync<T1>(T1 message, IPipe<SendContext> sendPipe) where T1 : class
    {
        throw new NotImplementedException();
    }

    public Task RespondAsync(object message)
    {
        throw new NotImplementedException();
    }

    public Task RespondAsync(object message, Type messageType)
    {
        throw new NotImplementedException();
    }

    public Task RespondAsync(object message, IPipe<SendContext> sendPipe)
    {
        throw new NotImplementedException();
    }

    public Task RespondAsync(object message, Type messageType, IPipe<SendContext> sendPipe)
    {
        throw new NotImplementedException();
    }

    public Task RespondAsync<T1>(object values) where T1 : class
    {
        throw new NotImplementedException();
    }

    public Task RespondAsync<T1>(object values, IPipe<SendContext<T1>> sendPipe) where T1 : class
    {
        throw new NotImplementedException();
    }

    public Task RespondAsync<T1>(object values, IPipe<SendContext> sendPipe) where T1 : class
    {
        throw new NotImplementedException();
    }

    public void Respond<T1>(T1 message) where T1 : class
    {
        throw new NotImplementedException();
    }

    public Task NotifyConsumed<T1>(ConsumeContext<T1> context, TimeSpan duration, string consumerType) where T1 : class
    {
        throw new NotImplementedException();
    }

    public Task NotifyFaulted<T1>(ConsumeContext<T1> context, TimeSpan duration, string consumerType, Exception exception) where T1 : class
    {
        throw new NotImplementedException();
    }

    public ReceiveContext ReceiveContext { get; }
    public SerializerContext SerializerContext { get; }
    public Task ConsumeCompleted { get; }
    public IEnumerable<string> SupportedMessageTypes { get; }
    public Task NotifyConsumed(TimeSpan duration, string consumerType)
    {
        throw new NotImplementedException();
    }

    public Task NotifyFaulted(TimeSpan duration, string consumerType, Exception exception)
    {
        throw new NotImplementedException();
    }

    public T Message { get; }
}