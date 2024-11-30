using EventSourcing.Aggregates;
using System.Security.Principal;

namespace EventSourcing;

public class AccountService(EventStore eventStore)
{
	private readonly EventStore _eventStore = eventStore;

	public Guid CreateAccount(string fullName, decimal initialBalance)
	{
		var account = new Account();
		account.CreateAccount(fullName, initialBalance);

		_eventStore.SaveEvents(account.Events);
		account.ClearUncommittedEvents();

		return account.Id;
	}

	public decimal DepositMoney(Guid accountId, decimal amount)
	{
		var account = _eventStore.GetAccount(accountId);

		account!.DepositMoney(amount);

		_eventStore.SaveEvents(account.Events);
		account.ClearUncommittedEvents();

		return account.Balance;
	}

	public decimal WithDrawMoney(Guid accountId, decimal amount)
	{
		var account = _eventStore.GetAccount(accountId);

		account?.Withdraw(amount);

		_eventStore.SaveEvents(account!.Events);
		account.ClearUncommittedEvents();

		return account.Balance;
	}

	public void GetAccountHistory(Guid accountId)
	{
		var account = _eventStore.GetAccount(accountId);

		_eventStore.GetHistory(account);
	}
}
