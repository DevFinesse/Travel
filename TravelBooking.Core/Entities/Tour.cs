using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Core.Entities
{
    public class Tour
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AvailableSlots { get; set; }
        public bool IsFeatured { get; set; } = false;
    }
}
