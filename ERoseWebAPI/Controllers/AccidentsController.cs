using ERoseWebAPI.Models;
using ERoseWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERoseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentsController : ControllerBase
    {
        private readonly IAccidentService _accidentService;

        public AccidentsController(IAccidentService AccidentService)
        {
            _accidentService = AccidentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Accident>> GetAccidentAsync(int id)
        {
            if (!await _accidentService.AccidentExistsAsync(id))
            {
                return NotFound($"No Accident with id {id}");
            }

            Accident? accident = await _accidentService.GetAccidentAsync(id);

            if (accident == null)
            {
                return NotFound($"No Accident with id {id}");
            }

            return Ok(accident);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accident>>> GetAccidentsAsync()
        {
            IEnumerable<Accident> accidents = await _accidentService.GetAccidentsAsync();

            if (accidents != null)
            {
                return Ok(accidents);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<Accident>> PostAccidentAsync(Accident accident)
        {
            Accident? newAccident = await _accidentService.PostAccidentAsync(accident);

            if (newAccident != null)
            {
                return Ok(newAccident);
            }
            else
            {
                return NoContent();
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Accident>> PutAccidentAsync(int id, Accident Accident)
        {
            if (id != Accident.Id)
            {
                return BadRequest();
            }
            if (!await _accidentService.AccidentExistsAsync(id))
            {
                return NotFound($"No Accident with id {id}");
            }

            Accident? updatedAccident = await _accidentService.PutAccidentAsync(Accident);

            return Ok(updatedAccident);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccidentAsync(int id)
        {
            if (await _accidentService.AccidentExistsAsync(id))
            {
                bool isDeleted = await _accidentService.DeleteAccidentAsync(id);

                if (isDeleted)
                {
                    return Ok();
                }
                return NoContent();
            }
            return NotFound($"No Accident with id {id}");
        }

    }
}
