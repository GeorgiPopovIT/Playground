namespace EventSourcing.Events;

public class AccountCreated : Event
{
	public required string FullName { get; init; }

	public required decimal InitialBalance { get; init; }
}
