using ERoseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ERoseWebAPI.Data
{
    public class ERoseDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ERoseDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Hero> Heroes { get; set; }
        public virtual DbSet<AccidentType> AccidentTypes { get; set; }
        public virtual DbSet<Hazard> Hazards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            string? connectionString = _configuration.GetConnectionString("ERoseDB");
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hero>().HasIndex(h => h.HeroName).IsUnique();
            modelBuilder.Entity<Hero>().HasIndex(h => h.Email).IsUnique();
            modelBuilder.Entity<Hero>().HasIndex(h => h.PhoneNumber).IsUnique();

            modelBuilder.Entity<AccidentType>().HasIndex(a => a.Name).IsUnique();

            modelBuilder.Entity<AccidentType>().HasMany(a => a.Heroes).WithMany(h => h.AccidentTypes);

            modelBuilder.Entity<Hazard>().HasOne(d => d.AccidentType).WithMany(a => a.Hazards);
        }
    }
}
