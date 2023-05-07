using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ERoseWebAPI.DTO.Requests;
using ERoseWebAPI.DTO.Responses;
using ERoseWebAPI.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace ERoseWebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IHeroService _heroService;

        public AuthService(IHeroService heroService, IConfiguration configuration)
        {
            _heroService = heroService;
            _configuration = configuration;
        }

        //</inheritdoc>
        public Task<LoginResponse> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        //</inheritdoc>
        public async Task<LoginResponse> LoginAsync(string phoneNumber, string password)
        {
            var dbHero = await _heroService.GetHeroesByPhoneNumberAsync(phoneNumber);

            if (dbHero != null)
            {
                if (PasswordHelper.VerifyPassword(password, dbHero.Password))
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new(ClaimTypes.MobilePhone, dbHero.PhoneNumber),
                    };

                    LoginResponse response = new()
                    {
                        Hero = new HeroResponse(dbHero),
                        Token = CreateJWT(_configuration["Jwt:Key"]!, claims),
                        StatusCode = StatusCodes.Status200OK,
                    };
                    return response;
                }
                return new LoginResponse()
                {
                    ErrorMessage = $"Password incorrect",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            return new LoginResponse()
            {
                ErrorMessage = $"No User with phone number : {phoneNumber} exists",
                StatusCode = StatusCodes.Status404NotFound,
            };
        }

        //</inheritdoc>
        public string CreateJWT(string secret, List<Claim> claims)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _configuration["Jwt:Issuer"],
            };

            var securityToken = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }

        //</inheritdoc>
        public string CreateRefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
