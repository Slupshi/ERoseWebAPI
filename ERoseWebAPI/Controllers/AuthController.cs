using ERoseWebAPI.Data;
using ERoseWebAPI.DTO.Requests;
using ERoseWebAPI.DTO.Responses;
using ERoseWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERoseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ERoseDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(ERoseDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            if (request.Email == null)
            {
                return BadRequest("L'email ne peut pas être vide");
            }
            LoginResponse response = await _authService.LoginAsync(request.Email, request.Password);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> RegisterAsync(RegisterRequest request)
        {
            return await _authService.RegisterAsync(request);
        }

    }
}
