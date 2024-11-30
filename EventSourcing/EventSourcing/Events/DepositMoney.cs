namespace EventSourcing.Events;

public class DepositMoney : Event
{
	public required decimal Amount { get; init; }
}
