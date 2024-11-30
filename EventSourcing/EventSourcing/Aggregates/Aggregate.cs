using EventSourcing.Events;

namespace EventSourcing.Aggregates;

public abstract class Aggregate
{
	public Guid Id { get; protected set; } 

	public abstract void ApplyEvent(Event @event);
}
