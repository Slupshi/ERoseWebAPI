namespace ERoseWebAPI.Models
{
    public class Hazard : ModelBase
    {
        public string CityName { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual AccidentType? AccidentType { get; set; }
    }
}
