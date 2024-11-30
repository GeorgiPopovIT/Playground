namespace EventSourcing.Events;

public class WithdrawMoney : Event
{
	public required decimal Amount { get; init; }
}
