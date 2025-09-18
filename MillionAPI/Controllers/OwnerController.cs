using Microsoft.AspNetCore.Mvc;
using Million.Application.DTOs;
using Million.Application.Interfaces;

namespace MillionAPI.Controllers;

[ApiController]
[Route("api/owners")]
public class OwnerController : ControllerBase
{
    private readonly IOwnerService _service;

    public OwnerController(IOwnerService service)
    {
        _service = service;
    }

    [HttpPost("find")]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> GetProperties([FromBody] OwnerFilterOptions filter)
    {
        var result = await _service.GetOwnersByFilterAsync(filter);
        return Ok(result);
    }
}