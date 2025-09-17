using Microsoft.AspNetCore.Mvc;
using Million.Application.Features.Properties.Services;
using Million.Application.DTOs;
using Million.Application.Interfaces;

namespace MillionAPI.Controllers;

[ApiController]
[Route("api/properties")]
public class PropertiesController : ControllerBase
{
    private readonly PropertyService _service;

    public PropertiesController(PropertyService service)
    {
        _service = service;
    }

    [HttpPost("find")]
    public async Task<ActionResult<IEnumerable<PropertyDto>>> GetProperties([FromBody] PropertyFilterOptions filter)
    {
        var result = await _service.GetPropertiesByFilterAsync(filter);
        return Ok(result);
    }
}