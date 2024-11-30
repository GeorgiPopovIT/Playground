namespace EventSourcing.Events;

public abstract class Event
{
	public Guid AggregateId { get; set; }

	public DateTimeOffset CreatedAtUtc { get; } = DateTimeOffset.UtcNow;

	public string JsonData { get; set; }
}
