using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Core.DTOs
{
    public class TourDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AvailableSlots { get; set; }
        public bool IsFeatured { get; set; }
    }

    public class CreateTourDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int AvailableSlots { get; set; }
        public bool IsFeatured { get; set; } = false;
    }
}
