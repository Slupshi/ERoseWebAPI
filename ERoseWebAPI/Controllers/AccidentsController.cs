using ERoseWebAPI.Data;
using ERoseWebAPI.Models;
using ERoseWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERoseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentsController : ControllerBase
    {
        private readonly ERoseDbContext _context;
        private readonly IAccidentService _accidentService;

        public AccidentsController(ERoseDbContext context, IAccidentService accidentService)
        {
            _context = context;
            _accidentService = accidentService;
        }

        // GET: api/Accidents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accident>>> GetAccidents()
        {
            if (_context.Accidents == null)
            {
                return NotFound();
            }
            return await _context.Accidents.ToListAsync();
        }

        // GET: api/Accidents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Accident>> GetAccident(int id)
        {
            if (_context.Accidents == null)
            {
                return NotFound();
            }
            var accident = await _context.Accidents.FindAsync(id);

            if (accident == null)
            {
                return NotFound();
            }

            return accident;
        }

        // PUT: api/Accidents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccident(int id, Accident accident)
        {
            if (id != accident.Id)
            {
                return BadRequest();
            }

            _context.Entry(accident).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccidentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accidents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Accident>> PostAccident(Accident accident)
        {
            if (_context.Accidents == null)
            {
                return Problem("Entity set 'ERoseDbContext.Accidents'  is null.");
            }
            _context.Accidents.Add(accident);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccident", new { id = accident.Id }, accident);
        }

        // DELETE: api/Accidents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccident(int id)
        {
            if (_context.Accidents == null)
            {
                return NotFound();
            }
            var accident = await _context.Accidents.FindAsync(id);
            if (accident == null)
            {
                return NotFound();
            }

            _context.Accidents.Remove(accident);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccidentExists(int id)
        {
            return (_context.Accidents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
