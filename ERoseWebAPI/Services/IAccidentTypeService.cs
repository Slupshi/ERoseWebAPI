using ERoseWebAPI.Models;

namespace ERoseWebAPI.Services
{
    public interface IAccidentTypeService
    {
        /// <summary>
        /// Get a AccidentType entity with its id
        /// </summary>
        /// <param name="id">AccidentType's id</param>
        /// <returns>An AccidentType entity</returns>
        public Task<AccidentType?> GetAccidentTypeAsync(int id);
        public Task<AccidentType?> GetAccidentTypeAsNoTrackingAsync(int id);
        /// <summary>
        /// Get all AccidentType entities
        /// </summary>
        /// <returns>An AccidentType entities collection</returns>
        public Task<IEnumerable<AccidentType?>> GetAccidentTypesAsync();
        /// <summary>
        /// Create a AccidentType entity in the database
        /// </summary>
        /// <param name="model">A AccidentType model</param>
        /// <returns>The new AccidentType entity</returns>
        public Task<AccidentType?> PostAccidentTypeAsync(AccidentType model);
        /// <summary>
        /// Update a AccidentType entity in the database
        /// </summary>
        /// <param name="model">An AccidentType model</param>
        /// <returns>The updated AccidentType entity</returns>
        public Task<AccidentType?> PutAccidentTypeAsync(AccidentType model);
        /// <summary>
        /// Delete a AccidentType entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boolean which show if the AccidentType entity is deleted or not</returns>
        public Task<bool> DeleteAccidentTypeAsync(int id);

        /// <summary>
        /// Determine if the AccidentType entity exists in the database with its id
        /// </summary>
        /// <param name="id">AccidentType's id</param>
        /// <returns>A boolean</returns>
        public Task<bool> AccidentTypeExistsAsync(int id);
    }
}
