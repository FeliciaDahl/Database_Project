using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Presentation_WebApi.Controllers;

[Route("api/projects")]
[ApiController]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    private readonly IProjectService _projectService = projectService;

    [HttpPost]

    public async Task<IActionResult> Create(ProjectRegistrationForm form)
    {
        if(ModelState.IsValid)
        {
            var project = await _projectService.CreateProjectAsync(form);
                return Ok(project);
        }  

            return BadRequest();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {

        return Ok(await _projectService.GetAllProjectsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetProject(int id)
    {
        var project = await _projectService.GetProjectAsync(p => p.Id == id);

        if (project == null)
            return NotFound();

        return Ok(project);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Project>> UpdateProject(int id, [FromBody] ProjectUpdateForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedProject = await _projectService.UpdateProjectAsync(id, form);
        if (updatedProject == null)
            return NotFound("Project not found.");

        return Ok(updatedProject); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var deleted = await _projectService.DeleteProjectAsync(id);
        if (!deleted)
            return NotFound("Project not found or could not be deleted.");

        return Ok(deleted); 
    }
}
