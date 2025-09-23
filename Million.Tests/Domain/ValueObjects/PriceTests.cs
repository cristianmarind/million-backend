using Million.Domain.ValueObjects;

namespace Million.Tests.Domain.ValueObjects;

[TestFixture]
public class PriceTests
{
    [Test]
    public void Constructor_WithValidAmountAndCurrency_CreatesPrice()
    {
        // Arrange
        var amount = 500000m;
        var currency = "USD";

        // Act
        var price = new Price(amount, currency);

        // Assert
        Assert.That(price.Amount, Is.EqualTo(amount));
        Assert.That(price.Currency, Is.EqualTo(currency));
    }

    [Test]
    public void Constructor_WithValidAmountAndDefaultCurrency_CreatesPrice()
    {
        // Arrange
        var amount = 500000m;

        // Act
        var price = new Price(amount);

        // Assert
        Assert.That(price.Amount, Is.EqualTo(amount));
        Assert.That(price.Currency, Is.EqualTo("USD"));
    }

    [Test]
    public void Constructor_WithZeroAmount_CreatesPrice()
    {
        // Arrange
        var amount = 0m;
        var currency = "USD";

        // Act
        var price = new Price(amount, currency);

        // Assert
        Assert.That(price.Amount, Is.EqualTo(amount));
        Assert.That(price.Currency, Is.EqualTo(currency));
    }

    [Test]
    public void Constructor_WithNegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        var amount = -100m;
        var currency = "USD";

        // Act & Assert
    var ex = Assert.Throws<Million.Domain.Exceptions.PropertyContentInvalidException>(() => new Price(amount, currency));

        Assert.That(ex.Message, Does.Contain("Price cannot be negative"));
    }

    [Test]
    public void Constructor_WithLargeAmount_CreatesPrice()
    {
        // Arrange
        var amount = 999999999.99m;
        var currency = "USD";

        // Act
        var price = new Price(amount, currency);

        // Assert
        Assert.That(price.Amount, Is.EqualTo(amount));
        Assert.That(price.Currency, Is.EqualTo(currency));
    }

    [Test]
    public void Constructor_WithDifferentCurrencies_CreatesPrice()
    {
        // Arrange
        var amount = 500000m;
        var currencies = new[] { "EUR", "GBP", "JPY", "CAD", "AUD" };

        foreach (var currency in currencies)
        {
            // Act
            var price = new Price(amount, currency);

            // Assert
            Assert.That(price.Amount, Is.EqualTo(amount));
            Assert.That(price.Currency, Is.EqualTo(currency));
        }
    }

    [Test]
    public void Constructor_WithEmptyCurrency_CreatesPrice()
    {
        // Arrange
        var amount = 500000m;
        var currency = "";

        // Act
        var price = new Price(amount, currency);

        // Assert
        Assert.That(price.Amount, Is.EqualTo(amount));
        Assert.That(price.Currency, Is.EqualTo(currency));
    }

    [Test]
    public void Constructor_WithNullCurrency_CreatesPrice()
    {
        // Arrange
        var amount = 500000m;
        string currency = null!;

        // Act
        var price = new Price(amount, currency);

        // Assert
        Assert.That(price.Amount, Is.EqualTo(amount));
        Assert.That(price.Currency, Is.Null);
    }
}
