using EventSourcing.Events;

namespace EventSourcing.Aggregates
{
	internal class Order : Aggregate
	{
		public int OrderNumber { get; private set; }

		public decimal TotalAmount { get; private set; }

		public Guid AccountId { get; private set; }

		public void CreateOrder(int orderNumber,Guid accountId)
		{
			//var 
		}
		public override void ApplyEvent(Event @event)
		{
			switch (@event)
			{

				default:
					break;
			}
		}
	}
}
