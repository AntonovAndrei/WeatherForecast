using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Entities;

namespace WeatherForecast.Domain
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Weather> Weathers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
