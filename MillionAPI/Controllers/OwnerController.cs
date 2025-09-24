using Microsoft.AspNetCore.Mvc;
using Million.Application.Common;
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
    public async Task<ActionResult<Result<IEnumerable<OwnerDto>>>> GetProperties([FromBody] OwnerFilterOptions filter)
    {
        if (filter.OwnerIdList.Count > 50)
            return BadRequest("El tamaño máximo permitido para pageSize es 50.");

        var result = await _service.GetOwnersByFilterAsync(filter);
        return Ok(result);
    }
}