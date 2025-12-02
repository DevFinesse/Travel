using Microsoft.EntityFrameworkCore;
using TravelBooking.Core.DTOs;
using TravelBooking.Core.Entities;
using TravelBooking.Core.Interfaces;
using TravelBooking.Infrastructure.Data;

namespace TravelBooking.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly TravelBookingDbContext _context;

        public BookingService(TravelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<BookingDto> CreateBookingAsync(int userId, CreateBookingDto createBookingDto)
        {
            // 1. Check if tour exists
            var tour = await _context.Tours.FindAsync(createBookingDto.TourId);
            if (tour == null)
            {
                throw new Exception("Tour not found");
            }

            // 2. Check availability
            if (tour.AvailableSlots <= 0)
            {
                throw new Exception("Tour is fully booked");
            }

            // 3. Prevent double booking (optional: check if user already booked this tour)
            var existingBooking = await _context.Bookings
                .AnyAsync(b => b.UserId == userId && b.TourId == createBookingDto.TourId && b.Status != "Cancelled");
            
            if (existingBooking)
            {
                throw new Exception("You have already booked this tour");
            }

            // 4. Create booking
            var booking = new Booking
            {
                UserId = userId,
                TourId = createBookingDto.TourId,
                BookingDate = DateTime.UtcNow,
                Status = "Confirmed"
            };

            // 5. Update available slots
            tour.AvailableSlots--;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Bookings.Add(booking);
                    _context.Tours.Update(tour);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            // Load user and tour for DTO
            await _context.Entry(booking).Reference(b => b.User).LoadAsync();
            await _context.Entry(booking).Reference(b => b.Tour).LoadAsync();

            return new BookingDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                UserName = booking.User?.Username ?? "Unknown",
                TourId = booking.TourId,
                TourName = booking.Tour?.Name ?? "Unknown",
                BookingDate = booking.BookingDate,
                Status = booking.Status
            };
        }

        public async Task<IEnumerable<BookingDto>> GetUserBookingsAsync(int userId)
        {
            return await _context.Bookings
                .Include(b => b.Tour)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    UserName = b.User.Username,
                    TourId = b.TourId,
                    TourName = b.Tour.Name,
                    BookingDate = b.BookingDate,
                    Status = b.Status
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.Tour)
                .Include(b => b.User)
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    UserName = b.User.Username,
                    TourId = b.TourId,
                    TourName = b.Tour.Name,
                    BookingDate = b.BookingDate,
                    Status = b.Status
                })
                .ToListAsync();
        }

        public async Task CancelBookingAsync(int bookingId, int userId, bool isAdmin = false)
        {
            var booking = await _context.Bookings
                .Include(b => b.Tour)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
            {
                throw new Exception("Booking not found");
            }

            // Check if user is authorized to cancel (owner or admin)
            if (!isAdmin && booking.UserId != userId) 
            {
                 throw new Exception("Unauthorized");
            }

            if (booking.Status == "Cancelled")
            {
                return;
            }

            booking.Status = "Cancelled";
            if (booking.Tour != null)
            {
                booking.Tour.AvailableSlots++;
            }

            await _context.SaveChangesAsync();
        }
    }
}
