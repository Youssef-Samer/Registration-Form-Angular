using System.Net;
using Microsoft.AspNetCore.Mvc;

using CitizensAPI.Core.Entities;
using CitizensAPI.Core.Interfaces;

namespace CitizensAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitizenDetailController : ControllerBase
{
    private readonly ICitizenRepository _repository;
    private readonly ILogger<CitizenDetailController> _logger;

    public CitizenDetailController(ICitizenRepositoryFactory citizenRepositoryFactory, ILogger<CitizenDetailController> logger)
    {
        _repository = citizenRepositoryFactory.Create();
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CitizenDetails>>> GetCitizensDetails()
    {
        var citizens = await _repository.GetAllAsync();
        if (!citizens.Any())
        {
            _logger.LogWarning("No citizen details found.");
            return NotFound(new { Message = "No citizen details found." });
        }

        return Ok(citizens);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CitizenDetails>> GetCitizenDetails(Guid id)
    {
        var citizenDetails = await _repository.GetByIdAsync(id);

        if (citizenDetails == null)
        {
            _logger.LogWarning($"Citizen with ID {id} not found.");
            return NotFound(new { Message = $"Citizen with ID {id} not found." });
        }

        return Ok(citizenDetails);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCitizenDetails(Guid id, CitizenDetails citizenDetails)
    {
        if (id != citizenDetails.CitizenId)
        {
            _logger.LogWarning($"Mismatched ID: {id} and CitizenId: {citizenDetails.CitizenId}");
            return BadRequest(new { Message = "The ID in the URL does not match the Citizen ID." });
        }

        if (!await _repository.ExistsAsync(id))
        {
            return NotFound(new { Message = $"Citizen with ID {id} not found for update." });
        }

        try
        {
            await _repository.UpdateAsync(citizenDetails);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating citizen with ID {id}.");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "An error occurred while updating the citizen details." });
        }
    }

    [HttpPost]
    public async Task<ActionResult<CitizenDetails>> AddCitizenDetails(CitizenDetails citizenDetails)
    {
        try
        {
            await _repository.AddAsync(citizenDetails);
            return CreatedAtAction(nameof(GetCitizenDetails), new { id = citizenDetails.CitizenId }, citizenDetails);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new citizen.");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "An error occurred while creating a new citizen." });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCitizenDetails(Guid id)
    {
        if (!await _repository.ExistsAsync(id))
        {
            _logger.LogWarning($"Citizen with ID {id} not found for deletion.");
            return NotFound(new { Message = $"Citizen with ID {id} not found for deletion." });
        }

        try
        {
            bool deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound(new { Message = "Citizen not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting citizen with ID {id}.");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "An error occurred while deleting the citizen." });
        }
    }
}
