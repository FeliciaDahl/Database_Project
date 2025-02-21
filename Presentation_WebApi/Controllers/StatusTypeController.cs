using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApi.Controllers;

[Route("api/status")]
[ApiController]
public class StatusTypeController(IStatusTypeService statusTypeService) : ControllerBase
{
    private readonly IStatusTypeService _statusTypeService = statusTypeService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StatusType>>> GetStatusType()
    {
        return Ok(await _statusTypeService.GetAllServicesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StatusType>> GetStatusType(int id)
    {
        var status = await _statusTypeService.GetStatusTypeByIdAsync(id);

        if (status == null)
            return NotFound();

        return Ok(status);
    }

}
