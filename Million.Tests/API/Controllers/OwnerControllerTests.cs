using Microsoft.AspNetCore.Mvc;
using Million.Application.DTOs;
using Million.Application.Features.Owners.Services;
using Million.Application.Interfaces;
using MillionAPI.Controllers;
using Moq;

namespace Million.Tests.API.Controllers;

[TestFixture]
public class OwnerControllerTests
{
    private Mock<IOwnerService> _mockService;
    private OwnerController _controller;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IOwnerService>();
        _controller = new OwnerController(_mockService.Object);
    }

    [Test]
    public async Task GetProperties_WithValidFilter_ReturnsOkResult()
    {
        // Arrange
        var filter = new OwnerFilterOptions
        {
            OwnerIdList = new List<string> { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
        };

        var expectedDtos = new List<OwnerDto>
        {
            new OwnerDto
            {
                Name = "John Doe",
                Address = "123 Main St, New York, USA",
                Photo = "john.jpg",
                Birthday = new DateTime(1990, 5, 15)
            },
            new OwnerDto
            {
                Name = "Jane Smith",
                Address = "456 Oak Ave, Los Angeles, USA",
                Photo = "jane.jpg",
                Birthday = new DateTime(1985, 8, 22)
            }
        };

    _mockService.Setup(s => s.GetOwnersByFilterAsync(filter))
           .ReturnsAsync(Million.Application.Common.Result<IEnumerable<OwnerDto>>.Ok(expectedDtos));

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
    Assert.That(okResult, Is.Not.Null);
    var resultValue = okResult.Value as Million.Application.Common.Result<IEnumerable<OwnerDto>>;
    Assert.That(resultValue, Is.Not.Null);
    Assert.That(resultValue!.Value, Is.EqualTo(expectedDtos));
    }

    [Test]
    public async Task GetProperties_WithEmptyResult_ReturnsOkResultWithEmptyCollection()
    {
        // Arrange
        var filter = new OwnerFilterOptions
        {
            OwnerIdList = new List<string> { "non-existent-id" }
        };

        var emptyDtos = new List<OwnerDto>();

    _mockService.Setup(s => s.GetOwnersByFilterAsync(filter))
           .ReturnsAsync(Million.Application.Common.Result<IEnumerable<OwnerDto>>.Ok(emptyDtos));

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
    Assert.That(okResult, Is.Not.Null);
    var resultValue = okResult.Value as Million.Application.Common.Result<IEnumerable<OwnerDto>>;
    Assert.That(resultValue, Is.Not.Null);
    Assert.That(resultValue!.Value, Is.EqualTo(emptyDtos));
    }

    [Test]
    public async Task GetProperties_WithNullFilter_ReturnsOkResult()
    {
        // Arrange
        OwnerFilterOptions filter = null!;
        var emptyDtos = new List<OwnerDto>();

    _mockService.Setup(s => s.GetOwnersByFilterAsync(filter))
           .ReturnsAsync(Million.Application.Common.Result<IEnumerable<OwnerDto>>.Ok(emptyDtos));

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
    Assert.That(okResult, Is.Not.Null);
    var resultValue = okResult.Value as Million.Application.Common.Result<IEnumerable<OwnerDto>>;
    Assert.That(resultValue, Is.Not.Null);
    Assert.That(resultValue!.Value, Is.EqualTo(emptyDtos));
    }

    [Test]
    public async Task GetProperties_WithEmptyOwnerIdList_ReturnsOkResultWithEmptyCollection()
    {
        // Arrange
        var filter = new OwnerFilterOptions
        {
            OwnerIdList = new List<string>()
        };

        var emptyDtos = new List<OwnerDto>();

    _mockService.Setup(s => s.GetOwnersByFilterAsync(filter))
           .ReturnsAsync(Million.Application.Common.Result<IEnumerable<OwnerDto>>.Ok(emptyDtos));

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
    Assert.That(okResult, Is.Not.Null);
    var resultValue = okResult.Value as Million.Application.Common.Result<IEnumerable<OwnerDto>>;
    Assert.That(resultValue, Is.Not.Null);
    Assert.That(resultValue!.Value, Is.EqualTo(emptyDtos));
    }

    [Test]
    public async Task GetProperties_WithSingleOwner_ReturnsOkResultWithSingleOwner()
    {
        // Arrange
        var filter = new OwnerFilterOptions
        {
            OwnerIdList = new List<string> { Guid.NewGuid().ToString() }
        };

        var expectedDtos = new List<OwnerDto>
        {
            new OwnerDto
            {
                Name = "John Doe",
                Address = "123 Main St, New York, USA",
                Photo = "john.jpg",
                Birthday = new DateTime(1990, 5, 15)
            }
        };

    _mockService.Setup(s => s.GetOwnersByFilterAsync(filter))
           .ReturnsAsync(Million.Application.Common.Result<IEnumerable<OwnerDto>>.Ok(expectedDtos));

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
    Assert.That(okResult, Is.Not.Null);
    var resultValue = okResult.Value as Million.Application.Common.Result<IEnumerable<OwnerDto>>;
    Assert.That(resultValue, Is.Not.Null);
    Assert.That(resultValue!.Value, Is.EqualTo(expectedDtos));
    }

    [Test]
    public async Task GetProperties_WithMultipleOwners_ReturnsOkResultWithAllOwners()
    {
        // Arrange
        var filter = new OwnerFilterOptions
        {
            OwnerIdList = new List<string> 
            { 
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString() 
            }
        };

        var expectedDtos = new List<OwnerDto>
        {
            new OwnerDto
            {
                Name = "John Doe",
                Address = "123 Main St, New York, USA",
                Photo = "john.jpg",
                Birthday = new DateTime(1990, 5, 15)
            },
            new OwnerDto
            {
                Name = "Jane Smith",
                Address = "456 Oak Ave, Los Angeles, USA",
                Photo = "jane.jpg",
                Birthday = new DateTime(1985, 8, 22)
            },
            new OwnerDto
            {
                Name = "Bob Johnson",
                Address = "789 Pine St, Chicago, USA",
                Photo = "bob.jpg",
                Birthday = new DateTime(1988, 12, 3)
            }
        };

    _mockService.Setup(s => s.GetOwnersByFilterAsync(filter))
           .ReturnsAsync(Million.Application.Common.Result<IEnumerable<OwnerDto>>.Ok(expectedDtos));

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
    Assert.That(okResult, Is.Not.Null);
    var resultValue = okResult.Value as Million.Application.Common.Result<IEnumerable<OwnerDto>>;
    Assert.That(resultValue, Is.Not.Null);
    Assert.That(resultValue!.Value, Is.EqualTo(expectedDtos));
    }

    [Test]
    public async Task GetProperties_WithNullPhoto_ReturnsOkResult()
    {
        // Arrange
        var filter = new OwnerFilterOptions
        {
            OwnerIdList = new List<string> { Guid.NewGuid().ToString() }
        };

        var expectedDtos = new List<OwnerDto>
        {
            new OwnerDto
            {
                Name = "John Doe",
                Address = "123 Main St, New York, USA",
                Photo = null!,
                Birthday = new DateTime(1990, 5, 15)
            }
        };

    _mockService.Setup(s => s.GetOwnersByFilterAsync(filter))
           .ReturnsAsync(Million.Application.Common.Result<IEnumerable<OwnerDto>>.Ok(expectedDtos));

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
    Assert.That(okResult, Is.Not.Null);
    var resultValue = okResult.Value as Million.Application.Common.Result<IEnumerable<OwnerDto>>;
    Assert.That(resultValue, Is.Not.Null);
    Assert.That(resultValue!.Value, Is.EqualTo(expectedDtos));
    }

    [Test]
    public async Task GetProperties_WithEmptyPhoto_ReturnsOkResult()
    {
        // Arrange
        var filter = new OwnerFilterOptions
        {
            OwnerIdList = new List<string> { Guid.NewGuid().ToString() }
        };

        var expectedDtos = new List<OwnerDto>
        {
            new OwnerDto
            {
                Name = "John Doe",
                Address = "123 Main St, New York, USA",
                Photo = "",
                Birthday = new DateTime(1990, 5, 15)
            }
        };

    _mockService.Setup(s => s.GetOwnersByFilterAsync(filter))
           .ReturnsAsync(Million.Application.Common.Result<IEnumerable<OwnerDto>>.Ok(expectedDtos));

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
    Assert.That(okResult, Is.Not.Null);
    var resultValue = okResult.Value as Million.Application.Common.Result<IEnumerable<OwnerDto>>;
    Assert.That(resultValue, Is.Not.Null);
    Assert.That(resultValue!.Value, Is.EqualTo(expectedDtos));
    }

    [Test]
    public async Task GetProperties_WithInvalidGuidFormat_ReturnsOkResult()
    {
        // Arrange
        var filter = new OwnerFilterOptions
        {
            OwnerIdList = new List<string> { "invalid-guid-format", "another-invalid-format" }
        };

        var emptyDtos = new List<OwnerDto>();

    _mockService.Setup(s => s.GetOwnersByFilterAsync(filter))
           .ReturnsAsync(Million.Application.Common.Result<IEnumerable<OwnerDto>>.Ok(emptyDtos));

        // Act
        var result = await _controller.GetProperties(filter);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        
        var okResult = result.Result as OkObjectResult;
    Assert.That(okResult, Is.Not.Null);
    var resultValue = okResult.Value as Million.Application.Common.Result<IEnumerable<OwnerDto>>;
    Assert.That(resultValue, Is.Not.Null);
    Assert.That(resultValue!.Value, Is.EqualTo(emptyDtos));
    }
}
