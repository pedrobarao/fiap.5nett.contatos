namespace Commons.Domain.Messages;

public abstract class Event : Message
{
    public Guid AggregateId { get; set; }
}