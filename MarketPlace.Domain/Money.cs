namespace MarketPlace.Domain;

public record class Money
{
    private const string DefaultCurrency = "EUR";

    public static Money FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup)
        => new Money(amount, currency, currencyLookup);

    public static Money FromString(string amount, string currency, ICurrencyLookup currencyLookup)
        => new Money(decimal.Parse(amount), currency, currencyLookup);


    protected Money(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
    {
        if (string.IsNullOrEmpty(currencyCode))
        {
            throw new ArgumentNullException(
                nameof(currencyCode), "Currency code must be specified");
        }

        var currency = currencyLookup.FindCurrency(currencyCode);
        if (!currency.InUse)
        {
            throw new ArgumentException(
              $"Currency {currencyCode} is not valid");
        }

        if (decimal.Round(amount, currency.DecimalPlaces) != amount)
        {
            throw new ArgumentOutOfRangeException(
            nameof(amount),
            $"Amount in {currencyCode} cannot have more than {currency.DecimalPlaces} decimals");
        }

        Amount = amount;
        Currency = currency;
    }

    internal Money(decimal amount, CurrencyDetails currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; }
    public CurrencyDetails Currency { get; }

    public Money Add(Money summand)
    {
        if (Currency != summand.Currency)
        {
            throw new CurrencyMismatchException(
              "Cannot sum amounts with different currencies");
        }

        return new Money(Amount + summand.Amount, Currency);
    }

    public Money Subtract(Money subtrahend)
    {
        if (Currency != subtrahend.Currency)
        {
            throw new CurrencyMismatchException(
              "Cannot subtract amounts with different currencies");
        }
        return new Money(Amount - subtrahend.Amount, Currency);
    }

    public static Money operator +(Money summand1, Money summand2) => summand1.Add(summand2);

    public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtract(subtrahend);

    public override string ToString() => $"{Currency.CurrencyCode} {Amount}";
}

public class CurrencyMismatchException : Exception
{
    public CurrencyMismatchException(string message) :
      base(message)
    {
    }
}
