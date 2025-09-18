using Million.Domain.Entities;
using Million.Domain.ValueObjects;

namespace Million.Tests.Domain.Entities;

[TestFixture]
public class PropertyTests
{
    private Address _validAddress;
    private Price _validPrice;
    private List<PropertyImage> _validImages;
    private List<PropertyTrace> _validTraces;
    private PresentationConfig _validPresentationConfig;
    private Guid _validOwnerId;

    [SetUp]
    public void Setup()
    {
        _validAddress = new Address("123 Main St", "New York", "USA");
        _validPrice = new Price(500000m, "USD");
        _validImages = new List<PropertyImage>
        {
            new PropertyImage("image1.jpg", true),
            new PropertyImage("image2.jpg", true)
        };
        _validTraces = new List<PropertyTrace>
        {
            new PropertyTrace(DateTime.Now.AddDays(-30), "Previous Owner", 450000m, 5000m)
        };
        _validPresentationConfig = new PresentationConfig(0, "premium");
        _validOwnerId = Guid.NewGuid();
    }

    [Test]
    public void Constructor_WithValidData_CreatesProperty()
    {
        // Arrange
        var name = "Beautiful House";
        var codeInternal = "PROP001";
        var year = 2020;

        // Act
        var property = new Property(
            name,
            _validAddress,
            _validPrice,
            _validOwnerId,
            codeInternal,
            year,
            _validImages,
            _validTraces,
            _validPresentationConfig
        );

        // Assert
        Assert.That(property.Name, Is.EqualTo(name));
        Assert.That(property.Address, Is.EqualTo(_validAddress));
        Assert.That(property.Price, Is.EqualTo(_validPrice));
        Assert.That(property.OwnerId, Is.EqualTo(_validOwnerId));
        Assert.That(property.CodeInternal, Is.EqualTo(codeInternal));
        Assert.That(property.Year, Is.EqualTo(year));
        Assert.That(property.PropertyImages, Is.EqualTo(_validImages));
        Assert.That(property.PropertyTraces, Is.EqualTo(_validTraces));
        Assert.That(property.PresentationConfig, Is.EqualTo(_validPresentationConfig));
        Assert.That(property.Id, Is.Not.EqualTo(Guid.Empty));
    }

    [Test]
    public void Constructor_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var name = "";
        var codeInternal = "PROP001";
        var year = 2020;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Property(
            name,
            _validAddress,
            _validPrice,
            _validOwnerId,
            codeInternal,
            year,
            _validImages,
            _validTraces,
            _validPresentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("Property name is required"));
    }

    [Test]
    public void Constructor_WithNullName_ThrowsArgumentException()
    {
        // Arrange
        string name = null!;
        var codeInternal = "PROP001";
        var year = 2020;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Property(
            name,
            _validAddress,
            _validPrice,
            _validOwnerId,
            codeInternal,
            year,
            _validImages,
            _validTraces,
            _validPresentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("Property name is required"));
    }

    [Test]
    public void Constructor_WithNullAddress_ThrowsArgumentNullException()
    {
        // Arrange
        var name = "Beautiful House";
        Address address = null!;
        var codeInternal = "PROP001";
        var year = 2020;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new Property(
            name,
            address,
            _validPrice,
            _validOwnerId,
            codeInternal,
            year,
            _validImages,
            _validTraces,
            _validPresentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("Address is required"));
    }

    [Test]
    public void Constructor_WithNullPrice_ThrowsArgumentNullException()
    {
        // Arrange
        var name = "Beautiful House";
        Price price = null!;
        var codeInternal = "PROP001";
        var year = 2020;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new Property(
            name,
            _validAddress,
            price,
            _validOwnerId,
            codeInternal,
            year,
            _validImages,
            _validTraces,
            _validPresentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("Price is required"));
    }

    [Test]
    public void Constructor_WithEmptyOwnerId_ThrowsArgumentException()
    {
        // Arrange
        var name = "Beautiful House";
        var ownerId = Guid.Empty;
        var codeInternal = "PROP001";
        var year = 2020;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Property(
            name,
            _validAddress,
            _validPrice,
            ownerId,
            codeInternal,
            year,
            _validImages,
            _validTraces,
            _validPresentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("OwnerId must be valid"));
    }

    [Test]
    public void Constructor_WithEmptyCodeInternal_ThrowsArgumentException()
    {
        // Arrange
        var name = "Beautiful House";
        var codeInternal = "";
        var year = 2020;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Property(
            name,
            _validAddress,
            _validPrice,
            _validOwnerId,
            codeInternal,
            year,
            _validImages,
            _validTraces,
            _validPresentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("Internal code is required"));
    }

    [Test]
    public void Constructor_WithInvalidYear_ThrowsArgumentException()
    {
        // Arrange
        var name = "Beautiful House";
        var codeInternal = "PROP001";
        var year = 1700; // Too old

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Property(
            name,
            _validAddress,
            _validPrice,
            _validOwnerId,
            codeInternal,
            year,
            _validImages,
            _validTraces,
            _validPresentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("Year must be valid"));
    }

    [Test]
    public void Constructor_WithFutureYear_ThrowsArgumentException()
    {
        // Arrange
        var name = "Beautiful House";
        var codeInternal = "PROP001";
        var year = DateTime.UtcNow.Year + 1; // Future year

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Property(
            name,
            _validAddress,
            _validPrice,
            _validOwnerId,
            codeInternal,
            year,
            _validImages,
            _validTraces,
            _validPresentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("Year must be valid"));
    }

    [Test]
    public void Constructor_WithNullImages_ThrowsArgumentNullException()
    {
        // Arrange
        var name = "Beautiful House";
        var codeInternal = "PROP001";
        var year = 2020;
        List<PropertyImage> images = null!;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new Property(
            name,
            _validAddress,
            _validPrice,
            _validOwnerId,
            codeInternal,
            year,
            images,
            _validTraces,
            _validPresentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("Property must have images"));
    }

    [Test]
    public void Constructor_WithEmptyImages_ThrowsArgumentNullException()
    {
        // Arrange
        var name = "Beautiful House";
        var codeInternal = "PROP001";
        var year = 2020;
        var images = new List<PropertyImage>();

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new Property(
            name,
            _validAddress,
            _validPrice,
            _validOwnerId,
            codeInternal,
            year,
            images,
            _validTraces,
            _validPresentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("Property must have images"));
    }

    [Test]
    public void Constructor_WithNullPresentationConfig_ThrowsArgumentNullException()
    {
        // Arrange
        var name = "Beautiful House";
        var codeInternal = "PROP001";
        var year = 2020;
        PresentationConfig presentationConfig = null!;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new Property(
            name,
            _validAddress,
            _validPrice,
            _validOwnerId,
            codeInternal,
            year,
            _validImages,
            _validTraces,
            presentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("PresentationConfig is required"));
    }

    [Test]
    public void Constructor_WithNullTraces_ThrowsArgumentNullException()
    {
        // Arrange
        var name = "Beautiful House";
        var codeInternal = "PROP001";
        var year = 2020;
        List<PropertyTrace> traces = null!;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new Property(
            name,
            _validAddress,
            _validPrice,
            _validOwnerId,
            codeInternal,
            year,
            _validImages,
            traces,
            _validPresentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("Property must have traces"));
    }

    [Test]
    public void Constructor_WithEmptyTraces_ThrowsArgumentNullException()
    {
        // Arrange
        var name = "Beautiful House";
        var codeInternal = "PROP001";
        var year = 2020;
        var traces = new List<PropertyTrace>();

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new Property(
            name,
            _validAddress,
            _validPrice,
            _validOwnerId,
            codeInternal,
            year,
            _validImages,
            traces,
            _validPresentationConfig
        ));

        Assert.That(ex.Message, Does.Contain("Property must have traces"));
    }
}
