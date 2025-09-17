namespace Million.Domain.ValueObjects;

public class Price
{
    public string Currency { get; }
    public decimal Amount { get; }

    public Price(decimal amount, string currency = "USD")
    {
        if (amount < 0) throw new ArgumentException("Price cannot be negative");

        Amount = amount;
        Currency = currency;
    }
}