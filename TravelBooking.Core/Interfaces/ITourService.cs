using TravelBooking.Core.DTOs;

namespace TravelBooking.Core.Interfaces
{
    public interface ITourService
    {
        Task<IEnumerable<TourDto>> GetAllToursAsync();
        Task<TourDto?> GetTourByIdAsync(int id);
        Task<TourDto> CreateTourAsync(CreateTourDto createTourDto);
        Task UpdateTourAsync(int id, CreateTourDto createTourDto);
        Task DeleteTourAsync(int id);
        Task<IEnumerable<TourDto>> SearchToursAsync(string? location, decimal? minPrice, decimal? maxPrice, DateTime? startDate);
    }
}
