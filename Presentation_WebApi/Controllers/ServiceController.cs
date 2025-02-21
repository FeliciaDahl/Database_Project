using Business.Dto;
using Business.Interfaces;
using Business.Models;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Service>>> GetServices()
    {
        return Ok(await _serviceService.GetAllServicesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Service>> GetServices(int id)
    {
        var service = await _serviceService.GetServiceAsync(p => p.Id == id);

        if (service == null)
            return NotFound();

        return Ok(service);
    }


    [HttpPut]
    public async Task<ActionResult<ProjectManager>> UpdateService(int id, [FromBody] ServiceUpdateForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedService = await _serviceService.UpdateServiceAsync(id, form);
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
