namespace TravelBooking.Core.DTOs
{
    public class AnalyticsDto
    {
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalTours { get; set; }
        public List<MonthlyBookingDto> BookingsPerMonth { get; set; } = new List<MonthlyBookingDto>();
    }

    public class MonthlyBookingDto
    {
        public string Month { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
