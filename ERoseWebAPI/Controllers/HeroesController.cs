using ERoseWebAPI.DTO.Responses;
using ERoseWebAPI.Models;
using ERoseWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERoseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroService _heroService;

        public HeroesController(IHeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HeroesResponse>> GetHeroAsync(int id)
        {
            if (!await _heroService.HeroExistsAsync(id))
            {
                return NotFound($"No Hero with id {id}");
            }

            Hero? hero = await _heroService.GetHeroAsync(id);

            if (hero == null)
            {
                return NotFound($"No Hero with id {id}");
            }

            return Ok(new HeroResponse(hero));
        }

        [HttpGet]
        public async Task<ActionResult<HeroesResponse>> GetHeroesAsync()
        {
            IEnumerable<Hero> heroes = await _heroService.GetHeroesAsync();

            HeroesResponse response = new();
            response.Items = new List<HeroResponse>();

            foreach (var hero in heroes)
            {
                response.Items.Add(new HeroResponse(hero));
            }

            return Ok(response);

        }

        [HttpPost]
        public async Task<ActionResult<HeroResponse>> PostHeroAsync(Hero hero)
        {
            if (hero.Accidents == null || !hero.Accidents.Any() || hero.Accidents.Count() > 3)
            {
                return BadRequest("Hero need between 1 and 3 (included) accident types");
            }
            if (!hero.Email.Contains('@'))
            {
                return BadRequest("Invalid Email");
            }

            Hero? newHero = await _heroService.PostHeroAsync(hero);

            HeroResponse response = new HeroResponse();

            if (newHero != null)
            {
                response = new HeroResponse(newHero);
                response.StatusCode = 201;
            }
            else
            {
                response.ErrorMessage = "Error while creating Hero";
                response.StatusCode = 500;
            }

            return StatusCode(response.StatusCode, response);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HeroResponse>> PutHeroAsync(int id, Hero hero)
        {
            if (id != hero.Id)
            {
                return BadRequest();
            }
            if (!await _heroService.HeroExistsAsync(id))
            {
                return NotFound($"No Hero with id {id}");
            }

            Hero? updatedHero = await _heroService.PutHeroAsync(hero);

            return Ok(updatedHero);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeroAsync(int id)
        {
            if (await _heroService.HeroExistsAsync(id))
            {
                bool isDeleted = await _heroService.DeleteHeroAsync(id);

                if (isDeleted)
                {
                    return Ok();
                }
                return NoContent();
            }
            return NotFound($"No Hero with id {id}");
        }

    }
}
