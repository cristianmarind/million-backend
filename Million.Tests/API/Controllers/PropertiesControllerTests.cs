using Microsoft.AspNetCore.Mvc;
using Million.Application.DTOs;
using Million.Application.Features.Properties.Services;
using Million.Application.Interfaces;
using Million.Domain.Entities;
using Million.Domain.ValueObjects;
using MillionAPI.Controllers;
using Moq;

namespace Million.Tests.API.Controllers;

[TestFixture]
public class PropertiesControllerTests
{
    private Mock<IPropertyService> _mockService;
    private PropertiesController _controller;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IPropertyService>();
        _controller = new PropertiesController(_mockService.Object);
    }

    [Test]
    public async Task GetProperties_WithValidFilter_ReturnsOkResult()
    {
        // Arrange
        var filter = new PropertyFilterOptions
        {
            Name = "Test Property",
            MinPrice = 100000m,
            MaxPrice = 500000m
        };

        var expectedDtos = new List<PropertyDto>
        {
            new PropertyDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Property 1",
                Address = "123 Main St, New York, USA",
                Price = 250000m,
                CodeInternal = "PROP001",
                Year = 2020,
                OwnerId = Guid.NewGuid(),
                ImageUrls = new List<string> { "image1.jpg", "image2.jpg" },
                PropertyTraces = new List<PropertyTraceDto>
                {
                    new PropertyTraceDto
                    {
                        Date = DateTime.Now.AddDays(-30),
                        Name = "Previous Owner",
                        Value = 200000m,
                        Tax = 2000m
                    }
                },
                PresentationConfig = new PresentationConfigDto
                {
                    CoverImageIndex = 0,
                    ListClass = "premium"
                }
            }
        };

        _mockService.Setup(s => s.GetPropertiesByFilterAsync(filter))
                   .ReturnsAsync(expectedDtos);

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(expectedDtos));
    }

    [Test]
    public async Task GetProperties_WithEmptyResult_ReturnsOkResultWithEmptyCollection()
    {
        // Arrange
        var filter = new PropertyFilterOptions
        {
            Name = "Non-existent Property"
        };

        var emptyDtos = new List<PropertyDto>();

        _mockService.Setup(s => s.GetPropertiesByFilterAsync(filter))
                   .ReturnsAsync(emptyDtos);

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(emptyDtos));
    }

    [Test]
    public async Task GetProperties_WithNullFilter_ReturnsOkResult()
    {
        // Arrange
        PropertyFilterOptions filter = null!;
        var emptyDtos = new List<PropertyDto>();

        _mockService.Setup(s => s.GetPropertiesByFilterAsync(filter))
                   .ReturnsAsync(emptyDtos);

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(emptyDtos));
    }

    [Test]
    public async Task GetProperties_WithMultipleProperties_ReturnsOkResultWithAllProperties()
    {
        // Arrange
        var filter = new PropertyFilterOptions();

        var expectedDtos = new List<PropertyDto>
        {
            new PropertyDto
            {
                Id = Guid.NewGuid(),
                Name = "Property 1",
                Address = "123 Main St, New York, USA",
                Price = 250000m,
                CodeInternal = "PROP001",
                Year = 2020,
                OwnerId = Guid.NewGuid(),
                ImageUrls = new List<string> { "image1.jpg" },
                PropertyTraces = new List<PropertyTraceDto>(),
                PresentationConfig = new PresentationConfigDto
                {
                    CoverImageIndex = 0,
                    ListClass = "premium"
                }
            },
            new PropertyDto
            {
                Id = Guid.NewGuid(),
                Name = "Property 2",
                Address = "456 Oak Ave, Los Angeles, USA",
                Price = 350000m,
                CodeInternal = "PROP002",
                Year = 2021,
                OwnerId = Guid.NewGuid(),
                ImageUrls = new List<string> { "image2.jpg" },
                PropertyTraces = new List<PropertyTraceDto>(),
                PresentationConfig = new PresentationConfigDto
                {
                    CoverImageIndex = 0,
                    ListClass = "standard"
                }
            }
        };

        _mockService.Setup(s => s.GetPropertiesByFilterAsync(filter))
                   .ReturnsAsync(expectedDtos);

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(expectedDtos));
    }

    [Test]
    public async Task GetProperties_WithPaginationFilter_ReturnsOkResult()
    {
        // Arrange
        var filter = new PropertyFilterOptions
        {
            Page = 2,
            PageSize = 5
        };

        var expectedDtos = new List<PropertyDto>
        {
            new PropertyDto
            {
                Id = Guid.NewGuid(),
                Name = "Property Page 2",
                Address = "789 Pine St, Chicago, USA",
                Price = 400000m,
                CodeInternal = "PROP003",
                Year = 2022,
                OwnerId = Guid.NewGuid(),
                ImageUrls = new List<string> { "image3.jpg" },
                PropertyTraces = new List<PropertyTraceDto>(),
                PresentationConfig = new PresentationConfigDto
                {
                    CoverImageIndex = 0,
                    ListClass = "luxury"
                }
            }
        };

        _mockService.Setup(s => s.GetPropertiesByFilterAsync(filter))
                   .ReturnsAsync(expectedDtos);

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(expectedDtos));
    }

    [Test]
    public async Task GetProperties_WithPriceRangeFilter_ReturnsOkResult()
    {
        // Arrange
        var filter = new PropertyFilterOptions
        {
            MinPrice = 200000m,
            MaxPrice = 300000m
        };

        var expectedDtos = new List<PropertyDto>
        {
            new PropertyDto
            {
                Id = Guid.NewGuid(),
                Name = "Mid-Range Property",
                Address = "321 Elm St, Boston, USA",
                Price = 275000m,
                CodeInternal = "PROP004",
                Year = 2019,
                OwnerId = Guid.NewGuid(),
                ImageUrls = new List<string> { "image4.jpg" },
                PropertyTraces = new List<PropertyTraceDto>(),
                PresentationConfig = new PresentationConfigDto
                {
                    CoverImageIndex = 0,
                    ListClass = "standard"
                }
            }
        };

        _mockService.Setup(s => s.GetPropertiesByFilterAsync(filter))
                   .ReturnsAsync(expectedDtos);

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(expectedDtos));
    }

    [Test]
    public async Task GetProperties_WithAddressFilter_ReturnsOkResult()
    {
        // Arrange
        var filter = new PropertyFilterOptions
        {
            Address = "New York"
        };

        var expectedDtos = new List<PropertyDto>
        {
            new PropertyDto
            {
                Id = Guid.NewGuid(),
                Name = "NYC Property",
                Address = "555 Broadway, New York, USA",
                Price = 600000m,
                CodeInternal = "PROP005",
                Year = 2023,
                OwnerId = Guid.NewGuid(),
                ImageUrls = new List<string> { "image5.jpg" },
                PropertyTraces = new List<PropertyTraceDto>(),
                PresentationConfig = new PresentationConfigDto
                {
                    CoverImageIndex = 0,
                    ListClass = "premium"
                }
            }
        };

        _mockService.Setup(s => s.GetPropertiesByFilterAsync(filter))
                   .ReturnsAsync(expectedDtos);

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(expectedDtos));
    }
}
