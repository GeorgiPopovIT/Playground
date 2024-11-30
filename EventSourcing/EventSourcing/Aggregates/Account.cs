using EventSourcing.Events;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace EventSourcing.Aggregates;

public class Account : Aggregate
{
	private readonly List<Event> _events = [];

	public string? FullName { get; private set; }
	public decimal Balance { get; private set; }

	public IEnumerable<Event> Events => _events;

	public void CreateAccount(string fullName, decimal initalBalance)
	{
		AccountCreated accountCreated = new()
		{
			AggregateId = Guid.NewGuid(),
			FullName = fullName,
			InitialBalance = initalBalance
		};
		ApplyEvent(accountCreated);
		_events.Add(accountCreated);
	}

	public void DepositMoney(decimal amount)
	{
		DepositMoney depositMoney = new()
		{
			AggregateId = Id,
			Amount = amount
		};

		ApplyEvent(depositMoney);
		_events.Add(depositMoney);
	}

	public void Withdraw(decimal amount)
	{
		if (Balance >= amount)
		{
			WithdrawMoney withdrawMoney = new() { AggregateId = Id, Amount = amount };
			ApplyEvent(withdrawMoney);
			_events.Add(withdrawMoney);
		}
		else
		{
			throw new InvalidOperationException("Insufficient funds");
		}
	}

	public override void ApplyEvent(Event @event)
	{
		switch (@event)
		{
			case AccountCreated accountCreated:
				Id = @event.AggregateId;
				FullName = accountCreated.FullName;
				Balance = accountCreated.InitialBalance;
				break;
			case DepositMoney amountMoney:
				Balance += amountMoney.Amount;
				break;
			case WithdrawMoney amountMoney:
				Balance -= amountMoney.Amount;
				break;
			default:
				throw new InvalidOperationException();
		}

		@event.JsonData = JsonSerializer.Serialize(this);
		//_events.Add(@event);
	}

	public void ClearUncommittedEvents() => _events.Clear();
}
