using ERoseWebAPI.Models;
using ERoseWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERoseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentTypesController : ControllerBase
    {
        private readonly IAccidentTypeService _accidentTypeService;

        public AccidentTypesController(IAccidentTypeService accidentTypeservice)
        {
            _accidentTypeService = accidentTypeservice;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccidentType>> GetAccidentTypeAsync(int id)
        {
            if (!await _accidentTypeService.AccidentTypeExistsAsync(id))
            {
                return NotFound($"No AccidentType with id {id}");
            }

            AccidentType? accidentType = await _accidentTypeService.GetAccidentTypeAsync(id);

            if (accidentType == null)
            {
                return NotFound($"No AccidentType with id {id}");
            }

            return Ok(accidentType);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccidentType>>> GetAccidentTypesAsync()
        {
            IEnumerable<AccidentType> accidentTypes = await _accidentTypeService.GetAccidentTypesAsync();

            if (accidentTypes != null)
            {
                return Ok(accidentTypes);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<AccidentType>> PostAccidentTypeAsync(AccidentType accident)
        {
            AccidentType? newAccident = await _accidentTypeService.PostAccidentTypeAsync(accident);

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
        public async Task<ActionResult<AccidentType>> PutAccidentTypeAsync(int id, AccidentType accidentType)
        {
            if (id != accidentType.Id)
            {
                return BadRequest();
            }
            if (!await _accidentTypeService.AccidentTypeExistsAsync(id))
            {
                return NotFound($"No Accident with id {id}");
            }

            AccidentType? updatedAccident = await _accidentTypeService.PutAccidentTypeAsync(accidentType);

            return Ok(updatedAccident);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccidentTypeAsync(int id)
        {
            if (await _accidentTypeService.AccidentTypeExistsAsync(id))
            {
                bool isDeleted = await _accidentTypeService.DeleteAccidentTypeAsync(id);

                if (isDeleted)
                {
                    return Ok();
                }
                return NoContent();
            }
            return NotFound($"No AccidentType with id {id}");
        }

    }
}
