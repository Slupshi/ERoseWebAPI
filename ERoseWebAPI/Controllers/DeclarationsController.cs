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

            Declaration? declarations = await _declarationService.GetDeclarationAsync(id);

            if (declarations == null)
            {
                return NotFound($"No Declaration with id {id}");
            }

            return Ok(declarations);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Declaration>>> GetDeclarationsAsync()
        {
            IEnumerable<Declaration> declarations = await _declarationService.GetDeclarationsAsync();

            if (declarations != null)
            {
                return Ok(declarations);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<Declaration>> PostDeclarationAsync(Declaration declaration)
        {
            Declaration? newDeclaration = await _declarationService.PostDeclarationAsync(declaration);

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
        public async Task<ActionResult<Declaration>> PutDeclarationAsync(int id, Declaration declaration)
        {
            if (id != declaration.Id)
            {
                return BadRequest();
            }
            if (!await _declarationService.DeclarationExistsAsync(id))
            {
                return NotFound($"No Declaration with id {id}");
            }

            Declaration? updatedDeclaration = await _declarationService.PutDeclarationAsync(declaration);

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
