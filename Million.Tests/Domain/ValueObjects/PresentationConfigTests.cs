using Million.Domain.ValueObjects;

namespace Million.Tests.Domain.ValueObjects;

[TestFixture]
public class PresentationConfigTests
{
    [Test]
    public void Constructor_WithValidData_CreatesPresentationConfig()
    {
        // Arrange
        var coverImageIndex = 0;
        var listClass = "premium";

        // Act
        var presentationConfig = new PresentationConfig(coverImageIndex, listClass);

        // Assert
        Assert.That(presentationConfig.CoverImageIndex, Is.EqualTo(coverImageIndex));
        Assert.That(presentationConfig.ListClass, Is.EqualTo(listClass));
    }

    [Test]
    public void Constructor_WithPositiveCoverImageIndex_CreatesPresentationConfig()
    {
        // Arrange
        var coverImageIndex = 5;
        var listClass = "standard";

        // Act
        var presentationConfig = new PresentationConfig(coverImageIndex, listClass);

        // Assert
        Assert.That(presentationConfig.CoverImageIndex, Is.EqualTo(coverImageIndex));
        Assert.That(presentationConfig.ListClass, Is.EqualTo(listClass));
    }

    [Test]
    public void Constructor_WithLargeCoverImageIndex_CreatesPresentationConfig()
    {
        // Arrange
        var coverImageIndex = 999;
        var listClass = "premium";

        // Act
        var presentationConfig = new PresentationConfig(coverImageIndex, listClass);

        // Assert
        Assert.That(presentationConfig.CoverImageIndex, Is.EqualTo(coverImageIndex));
        Assert.That(presentationConfig.ListClass, Is.EqualTo(listClass));
    }

    [Test]
    public void Constructor_WithNegativeCoverImageIndex_ThrowsArgumentException()
    {
        // Arrange
        var coverImageIndex = -1;
        var listClass = "premium";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PresentationConfig(coverImageIndex, listClass));

        Assert.That(ex.Message, Does.Contain("Cover image index cannot be negative"));
    }

    [Test]
    public void Constructor_WithEmptyListClass_ThrowsArgumentException()
    {
        // Arrange
        var coverImageIndex = 0;
        var listClass = "";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PresentationConfig(coverImageIndex, listClass));

        Assert.That(ex.Message, Does.Contain("ListClass is required"));
    }

    [Test]
    public void Constructor_WithNullListClass_ThrowsArgumentException()
    {
        // Arrange
        var coverImageIndex = 0;
        string listClass = null!;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PresentationConfig(coverImageIndex, listClass));

        Assert.That(ex.Message, Does.Contain("ListClass is required"));
    }

    [Test]
    public void Constructor_WithWhitespaceListClass_ThrowsArgumentException()
    {
        // Arrange
        var coverImageIndex = 0;
        var listClass = "   ";

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new PresentationConfig(coverImageIndex, listClass));

        Assert.That(ex.Message, Does.Contain("ListClass is required"));
    }

    [Test]
    public void Constructor_WithDifferentListClasses_CreatesPresentationConfig()
    {
        // Arrange
        var coverImageIndex = 0;
        var listClasses = new[] { "premium", "standard", "basic", "luxury", "economy" };

        foreach (var listClass in listClasses)
        {
            // Act
            var presentationConfig = new PresentationConfig(coverImageIndex, listClass);

            // Assert
            Assert.That(presentationConfig.ListClass, Is.EqualTo(listClass));
        }
    }

    [Test]
    public void Constructor_WithSpecialCharactersInListClass_CreatesPresentationConfig()
    {
        // Arrange
        var coverImageIndex = 0;
        var listClass = "premium-class_v2.1";

        // Act
        var presentationConfig = new PresentationConfig(coverImageIndex, listClass);

        // Assert
        Assert.That(presentationConfig.ListClass, Is.EqualTo(listClass));
    }

    [Test]
    public void Constructor_WithLongListClass_CreatesPresentationConfig()
    {
        // Arrange
        var coverImageIndex = 0;
        var listClass = "very_long_list_class_name_with_many_characters_and_numbers_123456789";

        // Act
        var presentationConfig = new PresentationConfig(coverImageIndex, listClass);

        // Assert
        Assert.That(presentationConfig.ListClass, Is.EqualTo(listClass));
    }
}
