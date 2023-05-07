using ERoseWebAPI.Data;
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

        [HttpPost("/login")]
        public void LoginAsync()
        {

        }

        [HttpPost("/register")]
        public void RegisterAsync()
        {

        }

    }
}
