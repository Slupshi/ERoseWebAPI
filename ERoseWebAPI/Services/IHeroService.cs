using ERoseWebAPI.Models;

namespace ERoseWebAPI.Services
{
    public interface IHeroService
    {
        /// <summary>
        /// Get a Hero entity with its id
        /// </summary>
        /// <param name="id">Hero's id</param>
        /// <returns>A Hero entity</returns>
        public Task<Hero?> GetHeroAsync(int id);
        /// <summary>
        /// Get all Heros entities who can endorse all accidents at once
        /// </summary>
        /// <param name="accidents">All accident type you search</param>
        /// <returns>A Hero entities collection</returns>
        public Task<IEnumerable<Hero?>> GetHeroesByAccidentTypeAsync(IEnumerable<Accident> accidents);
        /// <summary>
        /// Get a Hero with it's phone number
        /// </summary>
        /// <param name="phoneNumber">Desired phone number</param>
        /// <returns>A Hero entity</returns>
        public Task<Hero?> GetHeroesByPhoneNumberAsync(string phoneNumber);
        /// <summary>
        /// Get all Hero entities
        /// </summary>
        /// <returns>A Hero entities collection</returns>
        public Task<IEnumerable<Hero?>> GetHeroesAsync();
        /// <summary>
        /// Create a Hero entity in the database
        /// </summary>
        /// <param name="model">A Hero model</param>
        /// <returns>The new Hero entity</returns>
        public Task<Hero?> PostHeroAsync(Hero model);
        /// <summary>
        /// Update a Hero entity in the database
        /// </summary>
        /// <param name="model">A Hero model</param>
        /// <returns>The updated Hero entity</returns>
        public Task<Hero?> PutHeroAsync(Hero model);
        /// <summary>
        /// Delete a Hero entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boolean which show if the Hero entity is deleted or not</returns>
        public Task<bool> DeleteHeroAsync(int id);

        /// <summary>
        /// Determine if the Hero entity exists in the database with its id
        /// </summary>
        /// <param name="id">Hero's id</param>
        /// <returns>A boolean</returns>
        public Task<bool> HeroExistsAsync(int id);
    }
}
