using MarketPlace.Domain;

namespace MarketPlace.Tests;

public class MoneyTests
{
    private static readonly ICurrencyLookup CurrencyLookup = new FakeCurrencyLookup();

    [Fact]
    public void Two_of_same_amount_should_be_equal()
    {
        var firstAmount = Money.FromDecimal(10, "EUR", CurrencyLookup);
        var secondAmount = Money.FromDecimal(10, "EUR", CurrencyLookup);

        Assert.Equal(firstAmount, secondAmount);
    }

    [Fact]
    public void Two_of_same_amount_but_differentCurrencies()
    {
        var firstAmount = Money.FromDecimal(10, "USD", CurrencyLookup);
        var secondAmount = Money.FromDecimal(10, "EUR", CurrencyLookup);

        Assert.NotEqual(firstAmount, secondAmount);
    }

    [Fact]
    public void Sum_of_money_gives_full_amount()
    {
        var coin1 = Money.FromDecimal(1, "EUR", CurrencyLookup);
        var coin2 = Money.FromDecimal(2, "EUR", CurrencyLookup);
        var coin3 = Money.FromDecimal(2, "EUR", CurrencyLookup);
        var banknote = Money.FromDecimal(5, "EUR", CurrencyLookup);
        Assert.Equal(banknote, coin1 + coin2 + coin3);
    }
}
