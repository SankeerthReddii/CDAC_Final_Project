using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Metro_Ticket_Project.Models.DTOs.Auth;
using Metro_Ticket_Project.Models.DTOs.Complaint;
using Metro_Ticket_Project.Models.DTOs.Admin;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IMetroCardService _metroCardService;
        private readonly IComplaintService _complaintService;
        private readonly IPaymentService _paymentService;
        private readonly IAdminService _adminService;

        public AdminController(
            IMetroCardService metroCardService,
            IComplaintService complaintService,
            IPaymentService paymentService,
            IAdminService adminService)
        {
            _metroCardService = metroCardService;
            _complaintService = complaintService;
            _paymentService = paymentService;
            _adminService = adminService;
        }

        // Admin Sign in
        [HttpPost("sign_in")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
        {
            Console.WriteLine($"{request.Email} \n password --{request.Password}");
            var result = await _adminService.AdminLogInAsync(request.Email, request.Password);
            return Ok(result);
        }

        // Method to access Admin Dashboard Data
        [HttpGet("")]
        public async Task<IActionResult> FetchAllData()
        {
            var data = new AdminDataResponse
            {
                GetAllPendingComplaintsAsync = await _complaintService.GetAllPendingComplaintsAsync(),
                TotalCards = await _metroCardService.GetAllApprovedCardsAsync(),
                TotalComplaints = await _complaintService.GetAllComplaintsAsync(),
                TotalPendingCards = await _metroCardService.GetAllPendingCardRequestAsync(),
                TotalRecharge = await _paymentService.GetAllCardRechargeAsync(),
                TotalTickets = await _paymentService.GetAllTicketsAsync()
            };

            return Ok(data);
        }

        // Method to access approval pending cards data
        [HttpGet("issueCards")]
        public async Task<IActionResult> IssueCards()
        {
            var cards = await _metroCardService.FetchCardsAsync();
            return Ok(cards);
        }

        // Method to approve cards
        [HttpPut("issueCard/{id}")]
        public async Task<IActionResult> IssueCard(int id)
        {
            var result = await _metroCardService.IssueCardAsync(id);
            return Ok(result);
        }

        // Method to access all complaints including pending
        [HttpGet("complaints")]
        public async Task<IActionResult> DisplayComplaints()
        {
            var complaints = await _complaintService.DisplayAllComplaintsAsync();
            return Ok(complaints);
        }

        // Method to reply complaints
        [HttpPut("replyToComplaint/{id}")]
        public async Task<IActionResult> ReplyToComplaint(int id, [FromBody] ReplyToComplaint msgString)
        {
            Console.WriteLine($"msgString: {msgString?.MessageString}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (msgString == null || string.IsNullOrWhiteSpace(msgString.MessageString))
            {
                return BadRequest(new
                {
                    Message = "Reply message is required.",
                    Timestamp = DateTime.Now,
                    Success = false
                });
            }

            var result = await _complaintService.ReplyToComplaintsAsync(id, msgString);
            return Ok(result);
        }


    }
}