using TravelBooking.Core.DTOs;

namespace TravelBooking.Core.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> CreateBookingAsync(int userId, CreateBookingDto createBookingDto);
        Task<IEnumerable<BookingDto>> GetUserBookingsAsync(int userId);
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync(); // Admin only
        Task CancelBookingAsync(int bookingId, int userId, bool isAdmin = false);
    }
}
