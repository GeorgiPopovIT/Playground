using EventSourcing.Aggregates;
using EventSourcing.Events;

namespace EventSourcing;

public class EventStore
{
	private readonly Dictionary<Guid, SortedList<DateTimeOffset, Event>> _events = [];

	public void SaveEvents(IEnumerable<Event> events)
	{
		foreach (var @event in events)
		{
			var account = _events.GetValueOrDefault(@event.AggregateId);
			if (account is null)
			{
				_events[@event.AggregateId] = [];
			}

			_events[@event.AggregateId].Add(@event.CreatedAtUtc, @event);
		}
	}

	public Account? GetAccount(Guid aggregateId)
	{
		if (!_events.ContainsKey(aggregateId))
		{
			return null;
		}
		var account = new Account();
		var accountEvents = _events[aggregateId];
		foreach (var @event in accountEvents)
		{
			account.ApplyEvent(@event.Value);
		}

		return account;
	}

	public void GetHistory(Account account)
	{
		var accountEvents = _events[account.Id].Values;
		foreach (var e in accountEvents)
		{
			Console.WriteLine($"{e.GetType().Name} at {e.CreatedAtUtc} ; AggregateId: {e.AggregateId}");
		}
	}
}
