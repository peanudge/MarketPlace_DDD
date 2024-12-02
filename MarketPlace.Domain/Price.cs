namespace MarketPlace.Domain;

public record class Price : Money
{
    public static new Price FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) => new Price(amount, currency, currencyLookup);

    public Price(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
        : base(amount, currencyCode, currencyLookup)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Price cannot be negative", nameof(amount));
        }
    }
}
