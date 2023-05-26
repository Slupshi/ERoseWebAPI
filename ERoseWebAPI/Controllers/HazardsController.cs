using ERoseWebAPI.Models;
using ERoseWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERoseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HazardsController : ControllerBase
    {
        private readonly IHazardService _hazardService;

        public HazardsController(IHazardService hazardService)
        {
            _hazardService = hazardService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hazard>> GetHazardAsync(int id)
        {
            if (!await _hazardService.HazardExistsAsync(id))
            {
                return NotFound($"No Hazard with id {id}");
            }

            Hazard? hazard = await _hazardService.GetHazardAsync(id);

            if (hazard == null)
            {
                return NotFound($"No Hazard with id {id}");
            }

            return Ok(hazard);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hazard>>> GetHazardsAsync()
        {
            IEnumerable<Hazard> hazards = await _hazardService.GetHazardsAsync();

            if (hazards != null)
            {
                return Ok(hazards);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<Hazard>> PostHazardAsync(Hazard hazard)
        {
            Hazard? newHazard = await _hazardService.PostHazardAsync(hazard);

            if (newHazard != null)
            {
                return Ok(newHazard);
            }
            else
            {
                return NoContent();
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Hazard>> PutHazardAsync(int id, Hazard hazard)
        {
            if (id != hazard.Id)
            {
                return BadRequest();
            }
            if (!await _hazardService.HazardExistsAsync(id))
            {
                return NotFound($"No Hazard with id {id}");
            }

            Hazard? updatedHazard = await _hazardService.PutHazardAsync(hazard);

            return Ok(updatedHazard);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHazardAsync(int id)
        {
            if (await _hazardService.HazardExistsAsync(id))
            {
                bool isDeleted = await _hazardService.DeleteHazardAsync(id);

                if (isDeleted)
                {
                    return Ok();
                }
                return NoContent();
            }
            return NotFound($"No Hazard with id {id}");
        }

    }
}
