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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            string? connectionString = _configuration.GetConnectionString("CallMePhonyDB");
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hero>().HasIndex(h => h.HeroName).IsUnique();
            modelBuilder.Entity<Hero>().HasIndex(h => h.Email).IsUnique();
        }
    }
}
