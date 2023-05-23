using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ERoseWebAPI.DTO.Requests;
using ERoseWebAPI.DTO.Responses;
using ERoseWebAPI.Helpers;
using ERoseWebAPI.Models;
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
        public async Task<LoginResponse> RegisterAsync(RegisterRequest request)
        {
            Hero newHero = new()
            {
                HeroName = request.HeroName.Trim(),
                Email = request.Email.Trim(),
                Password = request.Password,
                FirstName = request.FirstName?.Trim(),
                LastName = request.LastName?.Trim(),
                PhoneNumber = request.PhoneNumber.Trim(),
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Accidents = request.Accidents,
            };

            Hero? dbHero = await _heroService.PostHeroAsync(newHero);

            if (PasswordHelper.VerifyPassword(request.Password, dbHero.Password))
            {
                List<Claim> claims = new List<Claim>
                    {
                        new(ClaimTypes.MobilePhone, dbHero.PhoneNumber),
                        new(ClaimTypes.Email, dbHero.Email),
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
                ErrorMessage = $"Internal Error",
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }

        //</inheritdoc>
        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var dbHero = await _heroService.GetHeroesByEmailAsync(email);

            if (dbHero != null)
            {
                if (PasswordHelper.VerifyPassword(password, dbHero.Password))
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new(ClaimTypes.MobilePhone, dbHero.PhoneNumber),
                        new(ClaimTypes.Email, dbHero.Email),
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
                ErrorMessage = $"No User with mail : {email} exists",
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
        public async Task<bool> IsHeroNameTaken(string heroName)
        {
            return (await _heroService.GetHeroesAsync()).Any(h => h != null && h.HeroName.Trim() == heroName.Trim());
        }

        //</inheritdoc>
        public async Task<bool> IsEmailTaken(string email)
        {
            return (await _heroService.GetHeroesAsync()).Any(h => h != null && h.Email.Trim() == email.Trim());
        }

        //</inheritdoc>
        public async Task<bool> IsPhoneNumberTaken(string phoneNumber)
        {
            return (await _heroService.GetHeroesAsync()).Any(h => h != null && h.PhoneNumber.Trim() == phoneNumber.Trim());
        }

        //</inheritdoc>
        public string CreateRefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
