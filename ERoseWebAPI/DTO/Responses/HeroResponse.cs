using ERoseWebAPI.Models;

namespace ERoseWebAPI.DTO.Responses
{
    public class HeroResponse : BaseResponse
    {
        public string HeroName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public int HeroScore { get; set; }

        public virtual IEnumerable<Accident>? Accidents { get; set; }

        public HeroResponse() { }

        public HeroResponse(Hero model)
        {
            HeroName = model.HeroName;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            PhoneNumber = model.PhoneNumber;
            Accidents = model.Accidents;
            Longitude = model.Longitude;
            Latitude = model.Latitude;
            HeroScore = model.HeroScore;
        }
    }
}
