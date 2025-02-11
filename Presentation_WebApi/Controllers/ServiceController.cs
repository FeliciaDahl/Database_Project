using Business.Dto;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApi.Controllers;

[Route("api/services")]
[ApiController]
public class ServiceController(IServiceService serviceService) : ControllerBase
{

    private readonly IServiceService _serviceService = serviceService;

    [HttpPost]
    public async Task<IActionResult> Create(ServiceRegistrationForm form)
    {
        if (ModelState.IsValid)
        {
            var service = await _serviceService.CreateServiceAsync(form);
            if (service != null)

                return Ok(service);
        }

        return BadRequest();
    }


    [HttpPut]
    public async Task<ActionResult<ProjectManager>> UpdateService([FromBody] ServiceUpdateForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedService = await _serviceService.UpdateServiceAsync(form);
        if (updatedService == null)
            return NotFound("Service not found.");

        return Ok(updatedService);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(int id)
    {
        var success = await _serviceService.DeleteServiceAsync(id);
        if (!success)
            return NotFound("Service not found or could not be deleted.");

        return NoContent();
    }
}
