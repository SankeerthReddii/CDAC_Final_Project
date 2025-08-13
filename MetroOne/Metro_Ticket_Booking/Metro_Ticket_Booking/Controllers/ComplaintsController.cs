using Metro_Ticket_Booking.DTOs; // Assuming DTOs are defined accordingly
using Metro_Ticket_Booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metro_Ticket_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly MetroTicketContext _context;

        public ComplaintsController(MetroTicketContext context)
        {
            _context = context;
        }

        // GET: api/Complaints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplaintResponseDto>>> GetComplaints()
        {
            try
            {
                var complaints = await _context.Complaints
                    .Include(c => c.User)
                    .Select(c => new ComplaintResponseDto
                    {
                        Id = c.ComplaintId,
                        UserId = c.UserId,
                        UserName = c.User.Name,
                        UserEmail = c.User.Email,
                        Subject = c.Message,
                        Reply = c.Reply,
                        SubmittedAt = c.SubmittedAt,
                        RepliedAt = c.RepliedAt
                    })
                    .OrderByDescending(c => c.SubmittedAt)
                    .ToListAsync();

                return Ok(complaints);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Complaints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplaintResponseDto>> GetComplaint(int id)
        {
            try
            {
                var complaint = await _context.Complaints
                    .Include(c => c.User)
                    .Where(c => c.ComplaintId == id)
                    .Select(c => new ComplaintResponseDto
                    {
                        Id = c.ComplaintId,
                        UserId = c.UserId,
                        UserName = c.User.Name,
                        UserEmail = c.User.Email,
                        Subject = c.Message,
                        Reply = c.Reply,
                        SubmittedAt = c.SubmittedAt,
                        RepliedAt = c.RepliedAt
                    })
                    .FirstOrDefaultAsync();

                if (complaint == null)
                    return NotFound(new { message = "Complaint not found" });

                return Ok(complaint);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/Complaints
        [HttpPost]
        public async Task<ActionResult<ComplaintResponseDto>> PostComplaint([FromBody] ComplaintCreateDto dto)
        {
            try
            {
                var complaint = new Complaint
                {
                    UserId = dto.UserId,
                    Message = dto.Description,
                    SubmittedAt = DateTime.UtcNow
                    // Reply and RepliedAt are null by default
                };

                _context.Complaints.Add(complaint);
                await _context.SaveChangesAsync();

                // Return created complaint with user info
                var result = await _context.Complaints
                    .Include(c => c.User)
                    .Where(c => c.ComplaintId == complaint.ComplaintId)
                    .Select(c => new ComplaintResponseDto
                    {
                        Id = c.ComplaintId,
                        UserId = c.UserId,
                        UserName = c.User.Name,
                        UserEmail = c.User.Email,
                        Subject = c.Message,
                        Reply = c.Reply,
                        SubmittedAt = c.SubmittedAt,
                        RepliedAt = c.RepliedAt
                    })
                    .FirstOrDefaultAsync();

                return CreatedAtAction(nameof(GetComplaint), new { id = complaint.ComplaintId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Complaints/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplaint(int id, [FromBody] ComplaintUpdateDto dto)
        {
            try
            {
                var complaint = await _context.Complaints.FindAsync(id);
                if (complaint == null)
                    return NotFound(new { message = "Complaint not found" });

                // Only allow updating Reply and RepliedAt for now, or extend as needed
                if (!string.IsNullOrEmpty(dto.Status))
                {
                    complaint.Reply = dto.Status;
                    complaint.RepliedAt = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Complaints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaint(int id)
        {
            try
            {
                var complaint = await _context.Complaints.FindAsync(id);
                if (complaint == null)
                    return NotFound(new { message = "Complaint not found" });

                _context.Complaints.Remove(complaint);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.ComplaintId == id);
        }
    }
}
