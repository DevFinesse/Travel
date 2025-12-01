using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Core.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int TourId { get; set; }
        public string TourName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class CreateBookingDto
    {
        [Required]
        public int TourId { get; set; }
    }
}
