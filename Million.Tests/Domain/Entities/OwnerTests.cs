using Million.Domain.Entities;
using Million.Domain.Exceptions;
using Million.Tests.Common;

namespace Million.Tests.Domain.Entities;

[TestFixture]
public class OwnerTests {
    [Test]
    public void Constructor_WithValidData_CreatesOwner() {
        var owner = TestValidData.OwnerData.GetValidOwner();

        Assert.That(owner.Name, Is.EqualTo(TestValidData.OwnerData.ValidName));
        Assert.That(owner.Address, Is.EqualTo(TestValidData.OwnerData.ValidAddress));
        Assert.That(owner.Photo, Is.EqualTo(TestValidData.OwnerData.ValidPhoto));
        Assert.That(owner.Birthday, Is.EqualTo(TestValidData.OwnerData.ValidBirthday));
        Assert.That(owner.Id, Is.Not.EqualTo(Guid.Empty));
    }

    [Test]
    public void Constructor_WithEmptyName_ThrowsPropertyNotFoundException() {
        // Arrange
        var name = "";
        var address = TestValidData.OwnerData.ValidAddress;
        var photo = TestValidData.OwnerData.ValidPhoto;
        var birthday = TestValidData.OwnerData.ValidBirthday;

        // Act & Assert
        var ex = Assert.Throws<PropertyNotFoundException>(() => new Owner(name, address, photo, birthday));

        Assert.That(ex.Message, Does.Contain("Owner name is required"));
    }

    [Test]
    public void Constructor_WithNullName_ThrowsPropertyNotFoundException() {
        // Arrange
        string name = null!;
        var address = TestValidData.OwnerData.ValidAddress;
        var photo = TestValidData.OwnerData.ValidPhoto;
        var birthday = TestValidData.OwnerData.ValidBirthday;

        // Act & Assert
        var ex = Assert.Throws<PropertyNotFoundException>(() => new Owner(name, address, photo, birthday));

        Assert.That(ex.Message, Does.Contain("Owner name is required"));
    }

    [Test]
    public void Constructor_WithWhitespaceName_ThrowsPropertyNotFoundException() {
        // Arrange
        var name = "   ";
        var address = TestValidData.OwnerData.ValidAddress;
        var photo = TestValidData.OwnerData.ValidPhoto;
        var birthday = TestValidData.OwnerData.ValidBirthday;

        // Act & Assert
        var ex = Assert.Throws<PropertyNotFoundException>(() => new Owner(name, address, photo, birthday));

        Assert.That(ex.Message, Does.Contain("Owner name is required"));
    }

    [Test]
    public void Constructor_WithEmptyAddress_ThrowsPropertyNotFoundException() {
        // Arrange
        var name = TestValidData.OwnerData.ValidName;
        var address = "";
        var photo = TestValidData.OwnerData.ValidPhoto;
        var birthday = TestValidData.OwnerData.ValidBirthday;

        // Act & Assert
        var ex = Assert.Throws<PropertyNotFoundException>(() => new Owner(name, address, photo, birthday));

        Assert.That(ex.Message, Does.Contain("Owner address is required"));
    }

    [Test]
    public void Constructor_WithNullAddress_ThrowsPropertyNotFoundException() {
        // Arrange
        var name = TestValidData.OwnerData.ValidName;
        string address = null!;
        var photo = TestValidData.OwnerData.ValidPhoto;
        var birthday = TestValidData.OwnerData.ValidBirthday;

        // Act & Assert
        var ex = Assert.Throws<PropertyNotFoundException>(() => new Owner(name, address, photo, birthday));

        Assert.That(ex.Message, Does.Contain("Owner address is required"));
    }

    [Test]
    public void Constructor_WithWhitespaceAddress_ThrowsPropertyNotFoundException() {
        // Arrange
        var name = TestValidData.OwnerData.ValidName;
        var address = "   ";
        var photo = TestValidData.OwnerData.ValidPhoto;
        var birthday = TestValidData.OwnerData.ValidBirthday;

        // Act & Assert
        var ex = Assert.Throws<PropertyNotFoundException>(() => new Owner(name, address, photo, birthday));

        Assert.That(ex.Message, Does.Contain("Owner address is required"));
    }

    [Test]
    public void Constructor_WithDefaultBirthday_ThrowsPropertyNotFoundException() {
        // Arrange
        var name = TestValidData.OwnerData.ValidName;
        var address = TestValidData.OwnerData.ValidAddress;
        var photo = TestValidData.OwnerData.ValidPhoto;
        var birthday = default(DateTime);

        // Act & Assert
        var ex = Assert.Throws<PropertyNotFoundException>(() => new Owner(name, address, photo, birthday));

        Assert.That(ex.Message, Does.Contain("Owner birthday must be valid"));
    }

    [Test]
    public void Constructor_WithNullPhoto_ThrowsPropertyNotFoundException() {
        // Arrange
        var name = TestValidData.OwnerData.ValidName;
        var address = TestValidData.OwnerData.ValidAddress;
        string photo = null; // Null photo should throw
        var birthday = TestValidData.OwnerData.ValidBirthday;

        // Act & Assert
        Assert.Throws<PropertyNotFoundException>(
            () => new Owner(name, address, photo, birthday),
            "Owner photo is required");
    }

    [Test]
    public void Constructor_WithEmptyPhoto_ThrowsPropertyNotFoundException() {
        // Arrange
        var name = TestValidData.OwnerData.ValidName;
        var address = TestValidData.OwnerData.ValidAddress;
        var photo = ""; // Empty photo should throw
        var birthday = TestValidData.OwnerData.ValidBirthday;

        // Act & Assert
        Assert.Throws<PropertyNotFoundException>(
            () => new Owner(name, address, photo, birthday),
            "Owner photo is required");
    }

    [Test]
    public void Constructor_WithWhitespacePhoto_ThrowsPropertyNotFoundException() {
        // Arrange
        var name = TestValidData.OwnerData.ValidName;
        var address = TestValidData.OwnerData.ValidAddress;
        var photo = "   "; // Whitespace photo should throw
        var birthday = TestValidData.OwnerData.ValidBirthday;

        // Act & Assert
        Assert.Throws<PropertyNotFoundException>(
            () => new Owner(name, address, photo, birthday),
            "Owner photo is required");
    }

    [Test]
    public void Constructor_WithInvalidPhotoUrl_ThrowsPropertyContentInvalidException() {
        // Arrange
        var name = TestValidData.OwnerData.ValidName;
        var address = TestValidData.OwnerData.ValidAddress;
        var photo = "invalid-url"; // Not a valid URL
        var birthday = TestValidData.OwnerData.ValidBirthday;

        // Act & Assert
        Assert.Throws<PropertyContentInvalidException>(
            () => new Owner(name, address, photo, birthday),
            "Owner photo must be a valid URL");
    }

    [Test]
    public void Constructor_WithValidPhotoUrl_CreatesOwner() {
        // Arrange
        var name = TestValidData.OwnerData.ValidName;
        var address = TestValidData.OwnerData.ValidAddress;
        var photo = TestValidData.OwnerData.ValidPhoto; // Valid URL
        var birthday = TestValidData.OwnerData.ValidBirthday;

        // Act
        var owner = new Owner(name, address, photo, birthday);

        // Assert
        Assert.That(owner.Photo, Is.EqualTo(photo));
    }

    [Test]
    public void Constructor_WithFutureBirthday_ThrowsPropertyContentInvalidException() {
        // Arrange
        var name = TestValidData.OwnerData.ValidName;
        var address = TestValidData.OwnerData.ValidAddress;
        var photo = TestValidData.OwnerData.ValidPhoto;
        var birthday = DateTime.Now.AddYears(1); // Future date

        // Act & Assert
        Assert.Throws<PropertyContentInvalidException>(
            () => new Owner(name, address, photo, birthday),
            "Owner birthday cannot be in the future");
    }

    [Test]
    public void Constructor_WithUnderageBirthday_ThrowsPropertyContentInvalidException() {
        // Arrange
        var name = TestValidData.OwnerData.ValidName;
        var address = TestValidData.OwnerData.ValidAddress;
        var photo = TestValidData.OwnerData.ValidPhoto;
        var birthday = DateTime.Now.AddYears(-17); // 17 years old

        // Act & Assert
        Assert.Throws<PropertyContentInvalidException>(
            () => new Owner(name, address, photo, birthday),
            "Owner must be at least 18 years old");
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesOwner() {
        // Arrange
        var name = TestValidData.OwnerData.ValidName;
        var address = TestValidData.OwnerData.ValidAddress;
        var photo = TestValidData.OwnerData.ValidPhoto;
        var birthday = TestValidData.OwnerData.ValidBirthday;

        // Act
        var owner = new Owner(name, address, photo, birthday);

        // Assert
        Assert.That(owner.Name, Is.EqualTo(name));
        Assert.That(owner.Address, Is.EqualTo(address));
        Assert.That(owner.Photo, Is.EqualTo(photo));
        Assert.That(owner.Birthday, Is.EqualTo(birthday));
    }
}
