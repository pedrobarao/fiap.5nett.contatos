namespace Commons.Domain.Messages;

public abstract class Message
{
    protected Message()
    {
        Timestamp = DateTime.Now;
        MessageType = GetType().Name;
    }

    public DateTime Timestamp { get; protected set; }
    public string MessageType { get; protected set; }
}