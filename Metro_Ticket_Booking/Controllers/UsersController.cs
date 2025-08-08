using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Metro_Ticket_Booking.DTOs;
using Metro_Ticket_Booking.Models;
using Metro_Ticket_Booking.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Metro_Ticket_Booking.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize(Roles = "user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet("stations")]
        public async Task<ActionResult<IEnumerable<Station>>> GetStations()
        {
            var stations = await _userService.GetAvailableStationsAsync();
            return Ok(stations);
        }

        [HttpGet("routes")]
        public async Task<ActionResult<IEnumerable<Models.Route>>> GetRoutes()
        {
            var routes = await _userService.GetAvailableRoutesAsync();
            return Ok(routes);
        }

        [HttpPost("tickets/book")]
        public async Task<ActionResult<Ticket>> BookTicket([FromBody] TicketBookingDto bookingDto)
        {
            bookingDto.UserId = GetUserId();

            var ticket = await _userService.BookTicketAsync(bookingDto);
            return Ok(ticket);
        }

        [HttpPost("metrocards/apply")]
        public async Task<IActionResult> ApplyMetroCard([FromQuery] int bookingId, [FromQuery] int metroCardId)
        {
            var result = await _userService.ApplyMetroCardToBookingAsync(bookingId, metroCardId);
            if (!result)
                return BadRequest("Failed to apply metrocard.");

            return Ok();
        }

        [HttpPost("metrocards/recharge")]
        public async Task<IActionResult> RechargeMetroCard([FromQuery] int metroCardId, [FromQuery] int amount)
        {
            var result = await _userService.RechargeMetroCardAsync(metroCardId, amount);
            if (!result)
                return BadRequest("Failed to recharge metrocard.");

            return Ok();
        }

        [HttpPost("complaints")]
        public async Task<ActionResult<Complaint>> RaiseComplaint([FromBody] ComplaintCreateDto complaintDto)
        {
            complaintDto.UserId = GetUserId();

            var complaint = await _userService.RaiseComplaintAsync(complaintDto);
            return Ok(complaint);
        }

        [HttpGet("complaints")]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetComplaints()
        {
            var userId = GetUserId().ToString();
            var complaints = await _userService.GetComplaintsByUserAsync(userId);
            return Ok(complaints);
        }

        [HttpGet("bookings/history")]
        public async Task<ActionResult<IEnumerable<BookingHistoryDto>>> GetBookingHistory()
        {
            var userId = GetUserId().ToString();
            var bookings = await _userService.GetBookingsByUserAsync(userId);
            return Ok(bookings);
        }
    }
}