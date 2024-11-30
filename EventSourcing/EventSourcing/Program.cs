using EventSourcing;

decimal amount;
var eventStore = new EventStore();
var accountService = new AccountService(eventStore);

var accountId = accountService.CreateAccount("Georgi Popov", 2344m);

amount = accountService.DepositMoney(accountId, 200m);
Console.WriteLine($"Balance: {amount}");

amount = accountService.WithDrawMoney(accountId, 80);
Console.WriteLine($"Balance: {amount}");


accountService.GetAccountHistory(accountId);


var dictionary = new Dictionary<string, int>()
{
	{ "Alex",12 },
	{ "Gosho", 39 }
};

var lookup = dictionary.GetAlternateLookup<ReadOnlySpan<char>>();
