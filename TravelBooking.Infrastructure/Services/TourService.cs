using Microsoft.EntityFrameworkCore;
using TravelBooking.Core.DTOs;
using TravelBooking.Core.Entities;
using TravelBooking.Core.Interfaces;
using TravelBooking.Infrastructure.Data;

namespace TravelBooking.Infrastructure.Services
{
    public class TourService : ITourService
    {
        private readonly TravelBookingDbContext _context;

        public TourService(TravelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourDto>> GetAllToursAsync()
        {
            return await _context.Tours
                .Select(t => new TourDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Location = t.Location,
                    Price = t.Price,
                    ImageUrl = t.ImageUrl,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    AvailableSlots = t.AvailableSlots,
                    IsFeatured = t.IsFeatured
                })
                .ToListAsync();
        }

        public async Task<TourDto?> GetTourByIdAsync(int id)
        {
            var t = await _context.Tours.FindAsync(id);
            if (t == null) return null;

            return new TourDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Location = t.Location,
                Price = t.Price,
                ImageUrl = t.ImageUrl,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                AvailableSlots = t.AvailableSlots,
                IsFeatured = t.IsFeatured
            };
        }

        public async Task<TourDto> CreateTourAsync(CreateTourDto createTourDto)
        {
            var tour = new Tour
            {
                Name = createTourDto.Name,
                Description = createTourDto.Description,
                Location = createTourDto.Location,
                Price = createTourDto.Price,
                ImageUrl = createTourDto.ImageUrl,
                StartDate = createTourDto.StartDate,
                EndDate = createTourDto.EndDate,
                AvailableSlots = createTourDto.AvailableSlots,
                IsFeatured = createTourDto.IsFeatured
            };

            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();

            return new TourDto
            {
                Id = tour.Id,
                Name = tour.Name,
                Description = tour.Description,
                Location = tour.Location,
                Price = tour.Price,
                ImageUrl = tour.ImageUrl,
                StartDate = tour.StartDate,
                EndDate = tour.EndDate,
                AvailableSlots = tour.AvailableSlots,
                IsFeatured = tour.IsFeatured
            };
        }

        public async Task UpdateTourAsync(int id, CreateTourDto createTourDto)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null) throw new Exception("Tour not found");

            tour.Name = createTourDto.Name;
            tour.Description = createTourDto.Description;
            tour.Location = createTourDto.Location;
            tour.Price = createTourDto.Price;
            tour.ImageUrl = createTourDto.ImageUrl;
            tour.StartDate = createTourDto.StartDate;
            tour.EndDate = createTourDto.EndDate;
            tour.AvailableSlots = createTourDto.AvailableSlots;
            tour.IsFeatured = createTourDto.IsFeatured;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTourAsync(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour != null)
            {
                _context.Tours.Remove(tour);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TourDto>> SearchToursAsync(string? location, decimal? minPrice, decimal? maxPrice, DateTime? startDate)
        {
            var query = _context.Tours.AsQueryable();

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(t => t.Location.Contains(location));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(t => t.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(t => t.Price <= maxPrice.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(t => t.StartDate >= startDate.Value);
            }

            return await query
                .Select(t => new TourDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Location = t.Location,
                    Price = t.Price,
                    ImageUrl = t.ImageUrl,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    AvailableSlots = t.AvailableSlots,
                    IsFeatured = t.IsFeatured
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TourDto>> GetFeaturedToursAsync()
        {
            return await _context.Tours
                .Where(t => t.IsFeatured)
                .OrderBy(t => t.StartDate)
                .Take(6)
                .Select(t => new TourDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Location = t.Location,
                    Price = t.Price,
                    ImageUrl = t.ImageUrl,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    AvailableSlots = t.AvailableSlots,
                    IsFeatured = t.IsFeatured
                })
                .ToListAsync();
        }

        public async Task ToggleFeaturedAsync(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null) throw new Exception("Tour not found");

            tour.IsFeatured = !tour.IsFeatured;
            await _context.SaveChangesAsync();
        }
    }
}
