namespace ERoseWebAPI.Models
{
    public class Accident : ModelBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? IconCode { get; set; }
        public string? IconFontFamily { get; set; }
        public string? IconFontPackage { get; set; }

        public virtual IEnumerable<Hero>? Heroes { get; set; }
    }
}
