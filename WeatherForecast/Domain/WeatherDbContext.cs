using Microsoft.EntityFrameworkCore;

namespace WeatherForecast.Domain
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
