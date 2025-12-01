using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Core.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int TourId { get; set; }
        public Tour? Tour { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled
    }
}
