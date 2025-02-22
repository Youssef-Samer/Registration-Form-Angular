using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CitizensAPI.Models;

namespace CitizensAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitizenDetailController : ControllerBase
{
    private readonly CitizenDetailsContext _context;
    private readonly ILogger<CitizenDetailController> _logger;

    public CitizenDetailController(CitizenDetailsContext context, ILogger<CitizenDetailController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CitizenDetails>>> GetCitizensDetails()
    {
        try
        {
            var citizens = await _context.CitizenDetails.ToListAsync();
            if (citizens == null || !citizens.Any())
            {
                _logger.LogWarning("No citizen details found.");
                return NotFound(new { Message = "No citizen details found." });
            }

            return Ok(citizens);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving citizen details.");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "An error occurred while retrieving citizen details." });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CitizenDetails>> GetCitizenDetails(Guid id)
    {
        try
        {
            var citizenDetails = await _context.CitizenDetails.FindAsync(id);

            if (citizenDetails == null)
            {
                _logger.LogWarning($"Citizen with ID {id} not found.");
                return NotFound(new { Message = $"Citizen with ID {id} not found." });
            }

            return Ok(citizenDetails);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while retrieving citizen details with ID {id}.");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = $"An error occurred while retrieving citizen details with ID {id}." });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCitizenDetails(Guid id, CitizenDetails citizenDetails)
    {
        if (id != citizenDetails.CitizenId)
        {
            _logger.LogWarning($"Mismatched ID: {id} and CitizenId: {citizenDetails.CitizenId}");
            return BadRequest(new { Message = "The ID in the URL does not match the Citizen ID." });
        }

        try
        {
            _context.Entry(citizenDetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CitizenDetailsExists(id))
            {
                _logger.LogWarning($"Citizen with ID {id} not found for update.");
                return NotFound(new { Message = $"Citizen with ID {id} not found for update." });
            }
            else
            {
                _logger.LogError($"Concurrency error occurred while updating citizen with ID {id}.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "A concurrency error occurred while updating the citizen details." });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating citizen with ID {id}.");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = $"An error occurred while updating citizen with ID {id}." });
        }
    }

    [HttpPost]
    public async Task<ActionResult<CitizenDetails>> AddCitizenDetails(CitizenDetails citizenDetails)
    {
        try
        {
            if (citizenDetails == null)
            {
                _logger.LogWarning("Received null citizen details.");
                return BadRequest(new { Message = "Invalid citizen details." });
            }

            _context.CitizenDetails.Add(citizenDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitizenDetails", new { id = citizenDetails.CitizenId }, citizenDetails);
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
        try
        {
            var citizenDetails = await _context.CitizenDetails.FindAsync(id);
            if (citizenDetails == null)
            {
                _logger.LogWarning($"Citizen with ID {id} not found for deletion.");
                return NotFound(new { Message = $"Citizen with ID {id} not found for deletion." });
            }

            _context.CitizenDetails.Remove(citizenDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting citizen with ID {id}.");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = $"An error occurred while deleting citizen with ID {id}." });
        }
    }

    private bool CitizenDetailsExists(Guid id)
    {
        return _context.CitizenDetails.Any(e => e.CitizenId == id);
    }
}
