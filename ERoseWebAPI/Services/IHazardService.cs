using ERoseWebAPI.Models;

namespace ERoseWebAPI.Services
{
    public interface IHazardService
    {
        /// <summary>
        /// Get a Hazard entity with its id
        /// </summary>
        /// <param name="id">Hazard's id</param>
        /// <returns>A Hazard entity</returns>
        public Task<Hazard?> GetHazardAsync(int id);
        /// <summary>
        /// Get all Hazard entities
        /// </summary>
        /// <returns>A Hazard entities collection</returns>
        public Task<IEnumerable<Hazard?>> GetHazardsAsync();
        /// <summary>
        /// Create a Hazard entity in the database
        /// </summary>
        /// <param name="model">A Hazard model</param>
        /// <returns>The new Hazard entity</returns>
        public Task<Hazard?> PostHazardAsync(Hazard model);
        /// <summary>
        /// Update a Hazard entity in the database
        /// </summary>
        /// <param name="model">A Hazard model</param>
        /// <returns>The updated Hazard entity</returns>
        public Task<Hazard?> PutHazardAsync(Hazard model);
        /// <summary>
        /// Delete a Hazard entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boolean which show if the Hazard entity is deleted or not</returns>
        public Task<bool> DeleteHazardAsync(int id);

        /// <summary>
        /// Determine if the Hazard entity exists in the database with its id
        /// </summary>
        /// <param name="id">Hazard's id</param>
        /// <returns>A boolean</returns>
        public Task<bool> HazardExistsAsync(int id);
    }
}
