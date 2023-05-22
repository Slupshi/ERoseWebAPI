﻿namespace ERoseWebAPI.Models
{
    public class Declaration : ModelBase
    {
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual Hero? Hero { get; set; }
        public virtual Accident? Accident { get; set; }
    }
}