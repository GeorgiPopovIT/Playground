namespace EventSourcing.Events
{
	public record CreateOrder(int OrderNumber, Guid AccountId);
}
