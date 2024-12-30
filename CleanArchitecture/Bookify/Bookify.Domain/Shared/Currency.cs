namespace Bookify.Domain.Shared;

public record Currency
{
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");
    internal static readonly Currency None = new("");
    private Currency(string currencyCode) => Code = currencyCode;

    public string Code { get; init; }

    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code)
            ?? throw new ApplicationException("The currency is invalid");
    }

    public static readonly IReadOnlyCollection<Currency> All = [Usd, Eur];
}