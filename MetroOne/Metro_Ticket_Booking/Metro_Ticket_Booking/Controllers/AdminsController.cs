using Metro_Ticket_Booking.DTOs;
using Metro_Ticket_Booking.Models;
using Metro_Ticket_Booking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Metro_Ticket_Booking.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("dashboard/metrics")]
        public async Task<ActionResult<AdminDashboardMetricsDto>> GetDashboardMetrics()
        {
            var metrics = await _adminService.GetDashboardMetricsAsync();
            return Ok(metrics);
        }

        #region Stations CRUD

        [HttpGet("stations")]
        public async Task<ActionResult<IEnumerable<Station>>> GetStations()
        {
            var stations = await _adminService.GetStationsAsync();
            return Ok(stations);
        }

        [HttpGet("stations/{id}")]
        public async Task<ActionResult<Station>> GetStation(int id)
        {
            var station = await _adminService.GetStationByIdAsync(id);
            if (station == null)
                return NotFound();

            return Ok(station);
        }

        [HttpPost("stations")]
        public async Task<ActionResult<Station>> CreateStation([FromBody] Station station)
        {
            var created = await _adminService.CreateStationAsync(station);
            return CreatedAtAction(nameof(GetStation), new { id = created.StationId }, created);
        }

        [HttpPut("stations/{id}")]
        public async Task<ActionResult<Station>> UpdateStation(int id, [FromBody] Station station)
        {
            var updated = await _adminService.UpdateStationAsync(id, station);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("stations/{id}")]
        public async Task<IActionResult> DeleteStation(int id)
        {
            var deleted = await _adminService.DeleteStationAsync(id);
            if (!deleted)
                return NotFound();
            return Ok(deleted);

            //return NoContent();
        }




        #endregion

        #region Metros CRUD (similar pattern)

        [HttpGet("metros")]
        public async Task<ActionResult<IEnumerable<Metro>>> GetMetros()
        {
            var metros = await _adminService.GetMetrosAsync();
            return Ok(metros);
        }

        [HttpGet("metros/{id}")]
        public async Task<ActionResult<Metro>> GetMetro(int id)
        {
            var metro = await _adminService.GetMetroByIdAsync(id);
            if (metro == null)
                return NotFound();

            return Ok(metro);
        }

        [HttpPost("metros")]
        public async Task<ActionResult<Metro>> CreateMetro([FromBody] Metro metro)
        {
            var created = await _adminService.CreateMetroAsync(metro);
            return CreatedAtAction(nameof(GetMetro), new { id = created.MetroId }, created);
        }

        [HttpPut("metros/{id}")]
        public async Task<ActionResult<Metro>> UpdateMetro(int id, [FromBody] Metro metro)
        {
            var updated = await _adminService.UpdateMetroAsync(id, metro);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("metros/{id}")]
        public async Task<IActionResult> DeleteMetro(int id)
        {
            var deleted = await _adminService.DeleteMetroAsync(id);
            if (!deleted)
                return NotFound();

            return Ok(deleted);
        }

        #endregion

        #region Routes CRUD

        [HttpGet("routes")]
        public async Task<ActionResult<IEnumerable<Models.Route>>> GetRoutes()
        {
            var routes = await _adminService.GetRoutesAsync();
            return Ok(routes);
        }

        [HttpGet("routes/{id}")]
        public async Task<ActionResult<Models.Route>> GetRoute(int id)
        {
            var route = await _adminService.GetRouteByIdAsync(id);
            if (route == null)
                return NotFound();

            return Ok(route);
        }

        [HttpPost("routes")]
        public async Task<ActionResult<Models.Route>> CreateRoute([FromBody] CreateOrUpdateRouteDto dto)
        {
            // Optionally validate IDs here exist
            var route = new Models.Route
            {
                Name = dto.Name,
                StartStationId = dto.StartStationId,
                EndStationId = dto.EndStationId
            };
            var created = await _adminService.CreateRouteAsync(route);
            return CreatedAtAction(nameof(GetRoute), new { id = created.RouteId }, created);
        }


        //[HttpPut("routes/{id}")]
        //public async Task<ActionResult<Models.Route>> UpdateRoute(int id, [FromBody] Models.Route route)
        //{
        //    var updated = await _adminService.UpdateRouteAsync(id, route);
        //    if (updated == null)
        //        return NotFound();

        //    return Ok(updated);
        //}

        [HttpPut("routes/{id}")]
        public async Task<ActionResult<Models.Route>> UpdateRoute(int id, [FromBody] CreateOrUpdateRouteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var routeToUpdate = new Models.Route
            {
                Name = dto.Name,
                StartStationId = dto.StartStationId,
                EndStationId = dto.EndStationId
            };

            var updated = await _adminService.UpdateRouteAsync(id, routeToUpdate);
            if (updated == null) return NotFound();

            return Ok(updated);
        }


        [HttpDelete("routes/{id}")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var deleted = await _adminService.DeleteRouteAsync(id);
            if (!deleted)
                return NotFound();

            return Ok(deleted);
        }

        #endregion

        #region Complaints Management

        [HttpGet("complaints/pending")]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetPendingComplaints()
        {
            var complaints = await _adminService.GetPendingComplaintsAsync();
            return Ok(complaints);
        }

        [HttpPut("complaints/{id}/status")]
        public async Task<IActionResult> UpdateComplaintStatus(int id, [FromBody] string status)
        {
            var updated = await _adminService.UpdateComplaintStatusAsync(id, status);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpPut("complaints/{id}/reply")]
        public async Task<IActionResult> ReplyToComplaint(int id, [FromBody] ReplyDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Reply))
            {
                return BadRequest("Reply text cannot be empty.");
            }

            // Fetch the complaint by id
            var complaint = await _adminService.GetComplaintByIdAsync(id);
            if (complaint == null)
            {
                return NotFound($"Complaint with id {id} not found.");
            }

            // Update reply and replied timestamp
            complaint.Reply = dto.Reply;
            complaint.RepliedAt = DateTime.UtcNow;

            // Save changes
            var updated = await _adminService.UpdateComplaintAsync(complaint);
            if (updated == null)
            {
                return StatusCode(500, "Failed to update complaint reply");
            }

            // Optional: send email notification to user about reply here

            return NoContent();
        }


        #endregion

        #region Metrocard Approvals

        [HttpGet("metrocards/pending")]
        public async Task<ActionResult<IEnumerable<MetroCard>>> GetPendingMetroCards()
        {
            var cards = await _adminService.GetPendingMetroCardApplicationsAsync();
            return Ok(cards);
        }

        [HttpPut("metrocards/{id}/approve")]
        public async Task<IActionResult> ApproveMetroCard(int id)
        {
            var approved = await _adminService.ApproveMetroCardAsync(id);
            if (!approved)
                return NotFound(new { message = "Metro card not found or could not be approved." });

            // Return some JSON object instead of just bool true
            return Ok(new { success = true, message = "Metro card approved successfully." });
        }

        [HttpPut("metrocards/{id}/reject")]
        public async Task<IActionResult> RejectMetroCard(int id)
        {
            var rejected = await _adminService.RejectMetroCardAsync(id);
            if (!rejected)
                return NotFound(new { message = "Metro card not found or could not be rejected." });

            return Ok(new { success = true, message = "Metro card rejected successfully." });
        }


        #endregion
    }
}