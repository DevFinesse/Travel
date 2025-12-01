using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Customer"; // Admin, Customer
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
