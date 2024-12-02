namespace MarketPlace.Domain;

public interface ICurrencyLookup
{
    CurrencyDetails FindCurrency(string currencyCode);
}

public record class CurrencyDetails
{
    public required string CurrencyCode { get; set; }
    public required bool InUse { get; set; }
    public required int DecimalPlaces { get; set; }

    public static CurrencyDetails None = new CurrencyDetails()
    {
        InUse = false,
        CurrencyCode = "None",
        DecimalPlaces = 2
    };
}
