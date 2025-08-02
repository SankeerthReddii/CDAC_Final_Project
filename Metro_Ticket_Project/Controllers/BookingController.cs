using Microsoft.AspNetCore.Mvc;
using Metro_Ticket_Project.Models.DTOs.Ticket;
using Metro_Ticket_Project.Models.DTOs.Common;
using Metro_Ticket_Project.Services.Interfaces;
using System.Text.Json;

namespace Metro_Ticket_Project.Controllers
{
    [ApiController]
    [Route("api/user/book_ticket")]
    public class BookingController : ControllerBase
    {
        private const string QR_CODE_IMAGE_PATH = "./MyQRCode.png";
        private readonly IBookingService _bookingService;
        private readonly IPaymentService _paymentService;
        private readonly Services.Utilities.SendMail _mailService;

        public BookingController(
            IBookingService bookingService,
            IPaymentService paymentService,
            Services.Utilities.SendMail mailService)
        {
            _bookingService = bookingService;
            _paymentService = paymentService;
            _mailService = mailService;
        }

        // Method to book ticket
        [HttpPost("journey_planner")]
        public async Task<IActionResult> BookTicket([FromBody] TicketRequest request)
        {
            if (request.DestinationId > 0 && request.SourceId > 0)
            {
                var response = await _bookingService.BookTicketAsync(request);

                try
                {
                    // QR Code generator for booked ticket
                    await GenerateQRCodeImageAsync(response.ToString(), 350, 350, QR_CODE_IMAGE_PATH);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Could not generate QR Code: {e.Message}");
                }

                return Ok(response);
            }
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid Source or destination!",
                    Timestamp = DateTime.Now
                });
            }
        }

        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrder([FromBody] Dictionary<string, object> data)
        {
            if (int.TryParse(data["amount"].ToString(), out int amount) && amount > 0)
            {
                var result = await _paymentService.PaymentAsync(data);
                Console.WriteLine(result.ToString());
                return Ok(result.ToString());
            }
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid amount!",
                    Timestamp = DateTime.Now
                });
            }
        }

        private async Task GenerateQRCodeImageAsync(string text, int width, int height, string filePath)
        {
            // Implementation for QR code generation
            // You would need to add a QR code library like QRCoder
            await Task.CompletedTask;
        }
    }
}