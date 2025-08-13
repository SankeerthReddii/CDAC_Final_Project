using Microsoft.AspNetCore.Mvc;
using Metro_Ticket_Project.Models.DTOs.Complaint;
using Metro_Ticket_Project.Services.Interfaces;
using Metro_Ticket_Project.Services.Utilities;
using Metro_Ticket_Project.Models.DTOs.User;

namespace Metro_Ticket_Project.Controllers
{
    [ApiController]
    [Route("api/user/complaints")]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _complaintService;
        private readonly SendMail _mailService;

        public ComplaintController(IComplaintService complaintService, SendMail mailService)
        {
            _complaintService = complaintService;
            _mailService = mailService;
        }

        [HttpPost("")]
        public async Task<IActionResult> DisplayAllComplaintsByEmail([FromBody] UserEmailRequest request)
        {
            var complaints = await _complaintService.DisplayAllComplaintsByEmailAsync(request);
            return Ok(complaints);
        }

        [HttpPost("register_complaint")]
        public async Task<IActionResult> RegisterComplaint([FromBody] ComplaintRequest complaint)
        {
            var result = await _complaintService.RegisterComplaintAsync(complaint);

            if (result == null)
                return StatusCode(500, "Complaint Registration failed!!!");

            // Uncomment when mail service is implemented
            // await _mailService.SendMailAsync(new NotificationEmail 
            // { 
            //     Subject = "Complaint Registered", 
            //     To = complaint.Email, 
            //     Body = "Your complaint has been registered successfully!!!" 
            // });

            return Ok("Complaint Register Successfully");
        }
    }
}