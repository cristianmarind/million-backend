using Microsoft.AspNetCore.Mvc;

using Million.Application.Common;
using Million.Application.DTOs;
using Million.Application.Interfaces;

namespace MillionAPI.Controllers;

[ApiController]
[Route("api/properties")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _service;

    public PropertiesController(IPropertyService service)
    {
        _service = service;
    }

    [HttpPost("find")]
    public async Task<ActionResult<Result<IEnumerable<PropertyDto>>>> GetProperties([FromBody] PropertyFilterOptions filter)
    {
        var result = await _service.GetPropertiesByFilterAsync(filter);
        return Ok(result);
    }
}