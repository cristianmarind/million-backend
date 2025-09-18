using Million.Domain.Entities;

namespace Million.Tests.Domain.ValueObjects;

[TestFixture]
public class PropertyTraceTests
{
    [Test]
    public void Constructor_WithValidData_CreatesPropertyTrace()
    {
        // Arrange
        var dateSale = new DateTime(2023, 5, 15);
        var name = "John Doe";
        var value = 450000m;
        var tax = 5000m;

        // Act
        var propertyTrace = new PropertyTrace(dateSale, name, value, tax);

        // Assert
        Assert.That(propertyTrace.DateSale, Is.EqualTo(dateSale));
        Assert.That(propertyTrace.Name, Is.EqualTo(name));
        Assert.That(propertyTrace.Value, Is.EqualTo(value));
        Assert.That(propertyTrace.Tax, Is.EqualTo(tax));
    }

    [Test]
    public void Constructor_WithDefaultDateSale_ThrowsArgumentException()
    {
        // Arrange
        var dateSale = default(DateTime);
        var name = "John Doe";
        var value = 450000m;
        var tax = 5000m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PropertyTrace(dateSale, name, value, tax));

        Assert.That(ex.Message, Does.Contain("DateSale must be valid"));
    }

    [Test]
    public void Constructor_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var dateSale = new DateTime(2023, 5, 15);
        var name = "";
        var value = 450000m;
        var tax = 5000m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PropertyTrace(dateSale, name, value, tax));

        Assert.That(ex.Message, Does.Contain("Name is required"));
    }

    [Test]
    public void Constructor_WithNullName_ThrowsArgumentException()
    {
        // Arrange
        var dateSale = new DateTime(2023, 5, 15);
        string name = null!;
        var value = 450000m;
        var tax = 5000m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PropertyTrace(dateSale, name, value, tax));

        Assert.That(ex.Message, Does.Contain("Name is required"));
    }

    [Test]
    public void Constructor_WithWhitespaceName_ThrowsArgumentException()
    {
        // Arrange
        var dateSale = new DateTime(2023, 5, 15);
        var name = "   ";
        var value = 450000m;
        var tax = 5000m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PropertyTrace(dateSale, name, value, tax));

        Assert.That(ex.Message, Does.Contain("Name is required"));
    }

    [Test]
    public void Constructor_WithZeroValue_ThrowsArgumentException()
    {
        // Arrange
        var dateSale = new DateTime(2023, 5, 15);
        var name = "John Doe";
        var value = 0m;
        var tax = 5000m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PropertyTrace(dateSale, name, value, tax));

        Assert.That(ex.Message, Does.Contain("Value must be positive"));
    }

    [Test]
    public void Constructor_WithNegativeValue_ThrowsArgumentException()
    {
        // Arrange
        var dateSale = new DateTime(2023, 5, 15);
        var name = "John Doe";
        var value = -1000m;
        var tax = 5000m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PropertyTrace(dateSale, name, value, tax));

        Assert.That(ex.Message, Does.Contain("Value must be positive"));
    }

    [Test]
    public void Constructor_WithNegativeTax_ThrowsArgumentException()
    {
        // Arrange
        var dateSale = new DateTime(2023, 5, 15);
        var name = "John Doe";
        var value = 450000m;
        var tax = -100m;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PropertyTrace(dateSale, name, value, tax));

        Assert.That(ex.Message, Does.Contain("Tax cannot be negative"));
    }

    [Test]
    public void Constructor_WithZeroTax_CreatesPropertyTrace()
    {
        // Arrange
        var dateSale = new DateTime(2023, 5, 15);
        var name = "John Doe";
        var value = 450000m;
        var tax = 0m;

        // Act
        var propertyTrace = new PropertyTrace(dateSale, name, value, tax);

        // Assert
        Assert.That(propertyTrace.Tax, Is.EqualTo(0m));
    }

    [Test]
    public void Constructor_WithLargeValues_CreatesPropertyTrace()
    {
        // Arrange
        var dateSale = new DateTime(2023, 5, 15);
        var name = "John Doe";
        var value = 999999999.99m;
        var tax = 999999.99m;

        // Act
        var propertyTrace = new PropertyTrace(dateSale, name, value, tax);

        // Assert
        Assert.That(propertyTrace.Value, Is.EqualTo(value));
        Assert.That(propertyTrace.Tax, Is.EqualTo(tax));
    }

    [Test]
    public void Constructor_WithFutureDate_CreatesPropertyTrace()
    {
        // Arrange
        var dateSale = DateTime.Now.AddDays(30);
        var name = "John Doe";
        var value = 450000m;
        var tax = 5000m;

        // Act
        var propertyTrace = new PropertyTrace(dateSale, name, value, tax);

        // Assert
        Assert.That(propertyTrace.DateSale, Is.EqualTo(dateSale));
    }

    [Test]
    public void Constructor_WithPastDate_CreatesPropertyTrace()
    {
        // Arrange
        var dateSale = DateTime.Now.AddYears(-10);
        var name = "John Doe";
        var value = 450000m;
        var tax = 5000m;

        // Act
        var propertyTrace = new PropertyTrace(dateSale, name, value, tax);

        // Assert
        Assert.That(propertyTrace.DateSale, Is.EqualTo(dateSale));
    }
}
