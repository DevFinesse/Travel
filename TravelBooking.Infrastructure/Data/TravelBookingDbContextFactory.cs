using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TravelBooking.Infrastructure.Data
{
    public class TravelBookingDbContextFactory : IDesignTimeDbContextFactory<TravelBookingDbContext>
    {
        public TravelBookingDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TravelBooking.API"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Build DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<TravelBookingDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TravelBookingDbContext(optionsBuilder.Options);
        }
    }
}
