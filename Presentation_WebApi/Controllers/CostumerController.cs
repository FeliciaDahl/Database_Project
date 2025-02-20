using Business.Dto;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_WebApi.Controllers;

[Route("api/costumers")]
[ApiController]
public class CostumerController(ICostumerService costumerService) : ControllerBase
{
    private readonly ICostumerService _costumerService = costumerService;

    [HttpPost]

    public async Task<IActionResult> Create(CostumerRegistrationForm form)
    {
        if (ModelState.IsValid)
        {
            var costumer = await _costumerService.CreateCostumerAsync(form);
            if (costumer != null)
                return Ok(costumer);
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Costumer>>> GetCostumer()
    {
        return Ok(await _costumerService.GetAllCostumersAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Costumer>> GetCostumer(int id)
    {
        var costumer = await _costumerService.GetCostumerAsync(p => p.Id == id);

        if (costumer == null)
            return NotFound();

        return Ok(costumer);
    }

    [HttpPut]
    public async Task<ActionResult<Costumer>> UpdateCostumer(int id, [FromBody] CostumerUpdateForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedCostumer = await _costumerService.UpdateCostumerAsync(id, form);
        if (updatedCostumer == null)
            return NotFound("Costumer not found.");

        return Ok(updatedCostumer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCostumer(int id)
    {
        var success = await _costumerService.DeleteCostumerAsync(id);
        if (!success)
            return NotFound("Costumer not found or could not be deleted.");

        return NoContent();
    }

}
