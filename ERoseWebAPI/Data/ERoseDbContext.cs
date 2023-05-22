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
        public virtual DbSet<Accident> Accidents { get; set; }
        public virtual DbSet<Declaration> Declarations { get; set; }

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

            modelBuilder.Entity<Accident>().HasIndex(a => a.Name).IsUnique();

            modelBuilder.Entity<Accident>().HasMany(a => a.Heroes).WithMany(h => h.Accidents);

            modelBuilder.Entity<Declaration>().HasOne(d => d.Hero).WithMany(h => h.Declarations);
            modelBuilder.Entity<Declaration>().HasOne(d => d.Accident).WithMany(a => a.Declarations);
        }
    }
}
