using Microsoft.AspNetCore.Mvc;
using Metro_Ticket_Project.Models.DTOs.Transaction;
using Metro_Ticket_Project.Services.Interfaces;
using Metro_Ticket_Project.Models.DTOs.User;

namespace Metro_Ticket_Project.Controllers
{
    [ApiController]
    [Route("api/user/history")]
    public class UserHistoryController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public UserHistoryController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("save")]
        public async Task<IActionResult> TransactionHistory([FromBody] TransactionRequest request)
        {
            var result = await _paymentService.BookingHistoryAsync(request);
            return Ok(result);
        }

        [HttpPost("display")]
        public async Task<IActionResult> DisplayTransactionHistory([FromBody] UserEmailRequest request)
        {
            var history = await _paymentService.DisplayHistoryAsync(request);
            return Ok(history);
        }
    }
}