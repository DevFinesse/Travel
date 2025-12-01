using TravelBooking.Core.DTOs;

namespace TravelBooking.Core.Interfaces
{
    public interface IAnalyticsService
    {
        Task<AnalyticsDto> GetAnalyticsAsync();
    }
}
