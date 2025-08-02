using Microsoft.AspNetCore.Mvc;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Controllers
{
    [ApiController]
    [Route("api/user/fare_and_schedule")]
    public class FareAndScheduleController : ControllerBase
    {
        private readonly ITripDetailsService _tripDetailsService;
        private readonly IBookingService _bookingService;

        public FareAndScheduleController(ITripDetailsService tripDetailsService, IBookingService bookingService)
        {
            _tripDetailsService = tripDetailsService;
            _bookingService = bookingService;
        }

        [HttpGet("fare/{id}")]
        public async Task<IActionResult> GetFareFromStation(int id)
        {
            var fare = await _bookingService.GetFairFromStationAsync(id);
            return Ok(fare);
        }

        [HttpGet("schedule/{id}")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var schedule = await _tripDetailsService.GetScheduleAsync(id);
            return Ok(schedule);
        }
    }
}