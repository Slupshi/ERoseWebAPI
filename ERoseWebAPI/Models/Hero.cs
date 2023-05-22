namespace ERoseWebAPI.Models
{
    public class Hero : ModelBase
    {
        public string HeroName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public int HeroScore { get; set; }

        public virtual IEnumerable<Accident>? Accidents { get; set; }
        public virtual IEnumerable<Declaration>? Declarations { get; set; }
    }
}
