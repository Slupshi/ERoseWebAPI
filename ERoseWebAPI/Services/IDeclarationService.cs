using ERoseWebAPI.Models;

namespace ERoseWebAPI.Services
{
    public interface IDeclarationService
    {
        /// <summary>
        /// Get a Declaration entity with its id
        /// </summary>
        /// <param name="id">Declaration's id</param>
        /// <returns>A Declaration entity</returns>
        public Task<Declaration?> GetDeclarationAsync(int id);
        /// <summary>
        /// Get all Declaration entities
        /// </summary>
        /// <returns>A Declaration entities collection</returns>
        public Task<IEnumerable<Declaration?>> GetDeclarationsAsync();
        /// <summary>
        /// Create a Declaration entity in the database
        /// </summary>
        /// <param name="model">A Declaration model</param>
        /// <returns>The new Declaration entity</returns>
        public Task<Declaration?> PostDeclarationAsync(Declaration model);
        /// <summary>
        /// Update a Declaration entity in the database
        /// </summary>
        /// <param name="model">A Declaration model</param>
        /// <returns>The updated Declaration entity</returns>
        public Task<Declaration?> PutDeclarationAsync(Declaration model);
        /// <summary>
        /// Delete a Declaration entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boolean which show if the Declaration entity is deleted or not</returns>
        public Task<bool> DeleteDeclarationAsync(int id);

        /// <summary>
        /// Determine if the Declaration entity exists in the database with its id
        /// </summary>
        /// <param name="id">Declaration's id</param>
        /// <returns>A boolean</returns>
        public Task<bool> DeclarationExistsAsync(int id);
    }
}
