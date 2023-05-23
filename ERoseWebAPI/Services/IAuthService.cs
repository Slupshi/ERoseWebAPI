using System.Security.Claims;
using ERoseWebAPI.DTO.Requests;
using ERoseWebAPI.DTO.Responses;

namespace ERoseWebAPI.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="request">Request with user's information</param>
        /// <returns>LoginResponse with JWT Token and User model</returns>
        public Task<LoginResponse> RegisterAsync(RegisterRequest request);
        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>LoginResponse with JWT Token and User model</returns>
        public Task<LoginResponse> LoginAsync(string email, string password);
        /// <summary>
        /// Create a Json Web Token
        /// </summary>
        /// <param name="secret">Where to keep JWT</param>
        /// <param name="claims">Informations kept in the JWT</param>
        /// <returns></returns>
        public string CreateJWT(string secret, List<Claim> claims);
        /// <summary>
        /// Create a refresh Token
        /// </summary>
        /// <returns>Not implemented yet</returns>
        public string CreateRefreshToken();
        /// <summary>
        /// Check if a user can use this heroname
        /// </summary>
        /// <param name="heroName">The user's desired name</param>
        /// <returns></returns>
        public Task<bool> IsHeroNameTaken(string heroName);
        /// <summary>
        /// Check if a user can use this email
        /// </summary>
        /// <param name="email">The user's desired email</param>
        /// <returns></returns>
        public Task<bool> IsEmailTaken(string email);
        /// <summary>
        /// Check if a user can use this phone number
        /// </summary>
        /// <param name="phoneNumber">The user's desired phone number</param>
        /// <returns></returns>
        public Task<bool> IsPhoneNumberTaken(string phoneNumber);
    }
}
