namespace ERoseWebAPI.Models
{
    public class Accident : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int IconCode { get; set; }
        public string IconFontFamily { get; set; }
        public string IconFontPakcage { get; set; }

        public virtual Hero? Hero { get; set; }
    }
}
