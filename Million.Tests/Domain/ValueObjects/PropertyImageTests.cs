using Million.Domain.Entities;

namespace Million.Tests.Domain.ValueObjects;

[TestFixture]
public class PropertyImageTests
{
    [Test]
    public void Constructor_WithValidFileAndEnabled_CreatesPropertyImage()
    {
        // Arrange
        var file = "image.jpg";
        var enabled = true;

        // Act
        var propertyImage = new PropertyImage(file, enabled);

        // Assert
        Assert.That(propertyImage.File, Is.EqualTo(file));
        Assert.That(propertyImage.Enabled, Is.EqualTo(enabled));
    }

    [Test]
    public void Constructor_WithValidFileAndDefaultEnabled_CreatesPropertyImage()
    {
        // Arrange
        var file = "image.jpg";

        // Act
        var propertyImage = new PropertyImage(file);

        // Assert
        Assert.That(propertyImage.File, Is.EqualTo(file));
        Assert.That(propertyImage.Enabled, Is.True);
    }

    [Test]
    public void Constructor_WithValidFileAndDisabled_CreatesPropertyImage()
    {
        // Arrange
        var file = "image.jpg";
        var enabled = false;

        // Act
        var propertyImage = new PropertyImage(file, enabled);

        // Assert
        Assert.That(propertyImage.File, Is.EqualTo(file));
        Assert.That(propertyImage.Enabled, Is.False);
    }

    [Test]
    public void Constructor_WithEmptyFile_ThrowsArgumentException()
    {
        // Arrange
        var file = "";
        var enabled = true;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PropertyImage(file, enabled));

        Assert.That(ex.Message, Does.Contain("File is required"));
    }

    [Test]
    public void Constructor_WithNullFile_ThrowsArgumentException()
    {
        // Arrange
        string file = null!;
        var enabled = true;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PropertyImage(file, enabled));

        Assert.That(ex.Message, Does.Contain("File is required"));
    }

    [Test]
    public void Constructor_WithWhitespaceFile_ThrowsArgumentException()
    {
        // Arrange
        var file = "   ";
        var enabled = true;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PropertyImage(file, enabled));

        Assert.That(ex.Message, Does.Contain("File is required"));
    }

    [Test]
    public void Constructor_WithDifferentFileExtensions_CreatesPropertyImage()
    {
        // Arrange
        var files = new[] { "image.jpg", "image.png", "image.gif", "image.webp", "image.bmp" };

        foreach (var file in files)
        {
            // Act
            var propertyImage = new PropertyImage(file);

            // Assert
            Assert.That(propertyImage.File, Is.EqualTo(file));
            Assert.That(propertyImage.Enabled, Is.True);
        }
    }

    [Test]
    public void Constructor_WithUrlFile_CreatesPropertyImage()
    {
        // Arrange
        var file = "https://example.com/images/property.jpg";

        // Act
        var propertyImage = new PropertyImage(file);

        // Assert
        Assert.That(propertyImage.File, Is.EqualTo(file));
        Assert.That(propertyImage.Enabled, Is.True);
    }

    [Test]
    public void Constructor_WithBase64File_CreatesPropertyImage()
    {
        // Arrange
        var file = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD...";

        // Act
        var propertyImage = new PropertyImage(file);

        // Assert
        Assert.That(propertyImage.File, Is.EqualTo(file));
        Assert.That(propertyImage.Enabled, Is.True);
    }

    [Test]
    public void Constructor_WithLongFileName_CreatesPropertyImage()
    {
        // Arrange
        var file = "very_long_property_image_filename_with_many_characters_and_numbers_123456789.jpg";

        // Act
        var propertyImage = new PropertyImage(file);

        // Assert
        Assert.That(propertyImage.File, Is.EqualTo(file));
        Assert.That(propertyImage.Enabled, Is.True);
    }
}
