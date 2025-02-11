using Business.Dto;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApi.Controllers;

[Route("api/projectManager")]
[ApiController]
public class ProjectManagerController(IProjectManagerService projectManagerService) : ControllerBase
{
    private readonly IProjectManagerService _projectManagerService = projectManagerService;

    [HttpPost]
    public async Task<IActionResult> Create(ProjectManagerRegistrationForm form)
    {
        if (ModelState.IsValid)
        {
            var projectManager = await _projectManagerService.CreateProjectManagerAsync(form);
            if (projectManager != null)

                return Ok(projectManager);
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectManager>>> GetProjects()
    {

        return Ok(await _projectManagerService.GetAllProjectManagersAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectManager>> GetProjectManager(int id)
    {
        var projectManager = await _projectManagerService.GetProjectManagerAsync(p => p.Id == id);

        if (projectManager == null)
            return NotFound();

        return Ok(projectManager);
    }

    [HttpPut]
    public async Task<ActionResult<ProjectManager>> UpdateProjectManager([FromBody] ProjectManagerUpdateForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedProjectManager = await _projectManagerService.UpdateProjectManagerAsync(form);
        if (updatedProjectManager == null)
            return NotFound("ProjectManager not found.");

        return Ok(updatedProjectManager);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectManager(int id)
    {
        var success = await _projectManagerService.DeleteProjectManagerAsync(id);
        if (!success)
            return NotFound("ProjectManager not found or could not be deleted.");

        return NoContent();
    }

}

