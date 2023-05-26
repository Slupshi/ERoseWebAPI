namespace ERoseWebAPI.Models
{
    public class AccidentType : ModelBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? IconCode { get; set; }
        public string? IconFontFamily { get; set; }
        public string? IconFontPackage { get; set; }

        public virtual IEnumerable<Hero>? Heroes { get; set; }
        public virtual IEnumerable<Hazard>? Hazards { get; set; }
    }
}
