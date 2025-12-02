using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelBooking.Core.DTOs;
using TravelBooking.Core.Interfaces;

namespace TravelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Create(CreateBookingDto createBookingDto)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    return Forbid("Admins cannot make bookings.");
                }

                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                var booking = await _bookingService.CreateBookingAsync(userId, createBookingDto);
                return CreatedAtAction(nameof(GetUserBookings), new { }, booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("my-bookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetUserBookings()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            return Ok(await _bookingService.GetUserBookingsAsync(userId));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings()
        {
            return Ok(await _bookingService.GetAllBookingsAsync());
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                var isAdmin = User.IsInRole("Admin");
                await _bookingService.CancelBookingAsync(id, userId, isAdmin);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
