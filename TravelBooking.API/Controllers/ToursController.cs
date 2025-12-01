using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBooking.Core.DTOs;
using TravelBooking.Core.Interfaces;

namespace TravelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _tourService;

        public ToursController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourDto>>> GetAll()
        {
            return Ok(await _tourService.GetAllToursAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TourDto>> GetById(int id)
        {
            var tour = await _tourService.GetTourByIdAsync(id);
            if (tour == null) return NotFound();
            return Ok(tour);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TourDto>> Create(CreateTourDto createTourDto)
        {
            var tour = await _tourService.CreateTourAsync(createTourDto);
            return CreatedAtAction(nameof(GetById), new { id = tour.Id }, tour);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, CreateTourDto createTourDto)
        {
            try
            {
                await _tourService.UpdateTourAsync(id, createTourDto);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tourService.DeleteTourAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TourDto>>> Search([FromQuery] string? location, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] DateTime? startDate)
        {
            return Ok(await _tourService.SearchToursAsync(location, minPrice, maxPrice, startDate));
        }
    }
}
