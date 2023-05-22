using ERoseWebAPI.Models;
using ERoseWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERoseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeclarationsController : ControllerBase
    {
        private readonly IDeclarationService _declarationService;

        public DeclarationsController(IDeclarationService declarationService)
        {
            _declarationService = declarationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Declaration>> GetDeclarationAsync(int id)
        {
            if (!await _declarationService.DeclarationExistsAsync(id))
            {
                return NotFound($"No Declaration with id {id}");
            }

            Declaration? Declaration = await _declarationService.GetDeclarationAsync(id);

            if (Declaration == null)
            {
                return NotFound($"No Declaration with id {id}");
            }

            return Ok(Declaration);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Declaration>>> GetDeclarationsAsync()
        {
            IEnumerable<Declaration> Declarations = await _declarationService.GetDeclarationsAsync();

            if (Declarations != null)
            {
                return Ok(Declarations);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<Declaration>> PostDeclarationAsync(Declaration Declaration)
        {
            Declaration? newDeclaration = await _declarationService.PostDeclarationAsync(Declaration);

            if (newDeclaration != null)
            {
                return Ok(newDeclaration);
            }
            else
            {
                return NoContent();
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Declaration>> PutDeclarationAsync(int id, Declaration Declaration)
        {
            if (id != Declaration.Id)
            {
                return BadRequest();
            }
            if (!await _declarationService.DeclarationExistsAsync(id))
            {
                return NotFound($"No Declaration with id {id}");
            }

            Declaration? updatedDeclaration = await _declarationService.PutDeclarationAsync(Declaration);

            return Ok(updatedDeclaration);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeclarationAsync(int id)
        {
            if (await _declarationService.DeclarationExistsAsync(id))
            {
                bool isDeleted = await _declarationService.DeleteDeclarationAsync(id);

                if (isDeleted)
                {
                    return Ok();
                }
                return NoContent();
            }
            return NotFound($"No Declaration with id {id}");
        }

    }
}
