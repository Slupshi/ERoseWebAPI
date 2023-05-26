using ERoseWebAPI.Models;

namespace ERoseWebAPI.DTO.Requests
{
    public class RegisterRequest
    {
        public string HeroName { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<AccidentType> Accidents { get; set; }
    }
}
