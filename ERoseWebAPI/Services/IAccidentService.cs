using ERoseWebAPI.Models;

namespace ERoseWebAPI.Services
{
    public interface IAccidentService
    {
        /// <summary>
        /// Get a Accident entity with its id
        /// </summary>
        /// <param name="id">Accident's id</param>
        /// <returns>A Accident entity</returns>
        public Task<Accident?> GetAccidentAsync(int id);
        public Task<Accident?> GetAccidentAsNoTrackingAsync(int id);
        /// <summary>
        /// Get all Accident entities
        /// </summary>
        /// <returns>A Accident entities collection</returns>
        public Task<IEnumerable<Accident?>> GetAccidentsAsync();
        /// <summary>
        /// Create a Accident entity in the database
        /// </summary>
        /// <param name="model">A Accident model</param>
        /// <returns>The new Accident entity</returns>
        public Task<Accident?> PostAccidentAsync(Accident model);
        /// <summary>
        /// Update a Accident entity in the database
        /// </summary>
        /// <param name="model">A Accident model</param>
        /// <returns>The updated Accident entity</returns>
        public Task<Accident?> PutAccidentAsync(Accident model);
        /// <summary>
        /// Delete a Accident entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boolean which show if the Accident entity is deleted or not</returns>
        public Task<bool> DeleteAccidentAsync(int id);

        /// <summary>
        /// Determine if the Accident entity exists in the database with its id
        /// </summary>
        /// <param name="id">Accident's id</param>
        /// <returns>A boolean</returns>
        public Task<bool> AccidentExistsAsync(int id);
    }
}
