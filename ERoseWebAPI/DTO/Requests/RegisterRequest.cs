namespace ERoseWebAPI.DTO.Requests
{
    public class RegisterRequest
    {
        public string HeroName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }

    }
}
