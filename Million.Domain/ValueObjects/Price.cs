using Million.Domain.Exceptions;

namespace Million.Domain.ValueObjects;

public class Price
{
    public string Currency { get; }
    public decimal Amount { get; }

    public Price(decimal amount, string currency = "USD")
    {
        if (amount < 0) throw new PropertyContentInvalidException("Price cannot be negative");
        /*
            Additional validations can be added here 
            1. Supported currency codes
            2. Decimal places based on currency
        */

        Amount = amount;
        Currency = currency;
    }
}