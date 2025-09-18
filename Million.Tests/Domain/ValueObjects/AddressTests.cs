using Million.Domain.ValueObjects;

namespace Million.Tests.Domain.ValueObjects;

[TestFixture]
public class AddressTests
{
    [Test]
    public void Constructor_WithValidData_CreatesAddress()
    {
        // Arrange
        var street = "123 Main St";
        var city = "New York";
        var country = "USA";

        // Act
        var address = new Address(street, city, country);

        // Assert
        Assert.That(address.Street, Is.EqualTo(street));
        Assert.That(address.City, Is.EqualTo(city));
        Assert.That(address.Country, Is.EqualTo(country));
    }

    [Test]
    public void Constructor_WithEmptyStreet_ThrowsArgumentException()
    {
        // Arrange
        var street = "";
        var city = "New York";
        var country = "USA";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Address(street, city, country));

        Assert.That(ex.Message, Does.Contain("Street is required"));
    }

    [Test]
    public void Constructor_WithNullStreet_ThrowsArgumentException()
    {
        // Arrange
        string street = null!;
        var city = "New York";
        var country = "USA";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Address(street, city, country));

        Assert.That(ex.Message, Does.Contain("Street is required"));
    }

    [Test]
    public void Constructor_WithWhitespaceStreet_ThrowsArgumentException()
    {
        // Arrange
        var street = "   ";
        var city = "New York";
        var country = "USA";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Address(street, city, country));

        Assert.That(ex.Message, Does.Contain("Street is required"));
    }

    [Test]
    public void Constructor_WithEmptyCity_ThrowsArgumentException()
    {
        // Arrange
        var street = "123 Main St";
        var city = "";
        var country = "USA";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Address(street, city, country));

        Assert.That(ex.Message, Does.Contain("City is required"));
    }

    [Test]
    public void Constructor_WithNullCity_ThrowsArgumentException()
    {
        // Arrange
        var street = "123 Main St";
        string city = null!;
        var country = "USA";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Address(street, city, country));

        Assert.That(ex.Message, Does.Contain("City is required"));
    }

    [Test]
    public void Constructor_WithWhitespaceCity_ThrowsArgumentException()
    {
        // Arrange
        var street = "123 Main St";
        var city = "   ";
        var country = "USA";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Address(street, city, country));

        Assert.That(ex.Message, Does.Contain("City is required"));
    }

    [Test]
    public void Constructor_WithEmptyCountry_ThrowsArgumentException()
    {
        // Arrange
        var street = "123 Main St";
        var city = "New York";
        var country = "";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Address(street, city, country));

        Assert.That(ex.Message, Does.Contain("Country is required"));
    }

    [Test]
    public void Constructor_WithNullCountry_ThrowsArgumentException()
    {
        // Arrange
        var street = "123 Main St";
        var city = "New York";
        string country = null!;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Address(street, city, country));

        Assert.That(ex.Message, Does.Contain("Country is required"));
    }

    [Test]
    public void Constructor_WithWhitespaceCountry_ThrowsArgumentException()
    {
        // Arrange
        var street = "123 Main St";
        var city = "New York";
        var country = "   ";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Address(street, city, country));

        Assert.That(ex.Message, Does.Contain("Country is required"));
    }

    [Test]
    public void Constructor_WithSpecialCharacters_CreatesAddress()
    {
        // Arrange
        var street = "123 Main St. #4B";
        var city = "São Paulo";
        var country = "Brasil";

        // Act
        var address = new Address(street, city, country);

        // Assert
        Assert.That(address.Street, Is.EqualTo(street));
        Assert.That(address.City, Is.EqualTo(city));
        Assert.That(address.Country, Is.EqualTo(country));
    }
}
