using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TravelBooking.Core.DTOs;
using TravelBooking.Core.Interfaces;
using TravelBooking.Infrastructure.Data;

namespace TravelBooking.Infrastructure.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly TravelBookingDbContext _context;

        public AnalyticsService(TravelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<AnalyticsDto> GetAnalyticsAsync()
        {
            var totalBookings = await _context.Bookings.CountAsync();
            var totalTours = await _context.Tours.CountAsync();
            
            // Calculate revenue from confirmed bookings
            var totalRevenue = await _context.Bookings
                .Where(b => b.Status == "Confirmed")
                .Include(b => b.Tour)
                .SumAsync(b => b.Tour.Price);

            // Group bookings by month for the last 12 months
            var lastYear = DateTime.UtcNow.AddYears(-1);
            var bookingsPerMonth = await _context.Bookings
                .Where(b => b.BookingDate >= lastYear)
                .GroupBy(b => new { b.BookingDate.Year, b.BookingDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToListAsync();

            var monthlyBookings = bookingsPerMonth.Select(x => new MonthlyBookingDto
            {
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.Month) + " " + x.Year,
                Count = x.Count
            }).ToList();

            return new AnalyticsDto
            {
                TotalBookings = totalBookings,
                TotalRevenue = totalRevenue,
                TotalTours = totalTours,
                BookingsPerMonth = monthlyBookings
            };
        }
    }
}
