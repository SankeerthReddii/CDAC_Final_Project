//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Metro_Ticket_Booking.Models;

//namespace Metro_Ticket_Booking.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PaymentsController : ControllerBase
//    {
//        private readonly MetroTicketContext _context;

//        public PaymentsController(MetroTicketContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Payments
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
//        {
//            return await _context.Payments.ToListAsync();
//        }

//        // GET: api/Payments/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Payment>> GetPayment(int id)
//        {
//            var payment = await _context.Payments.FindAsync(id);

//            if (payment == null)
//            {
//                return NotFound();
//            }

//            return payment;
//        }

//        // PUT: api/Payments/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutPayment(int id, Payment payment)
//        {
//            if (id != payment.PaymentId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(payment).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!PaymentExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Payments
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
//        {
//            _context.Payments.Add(payment);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetPayment", new { id = payment.PaymentId }, payment);
//        }

//        // DELETE: api/Payments/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePayment(int id)
//        {
//            var payment = await _context.Payments.FindAsync(id);
//            if (payment == null)
//            {
//                return NotFound();
//            }

//            _context.Payments.Remove(payment);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool PaymentExists(int id)
//        {
//            return _context.Payments.Any(e => e.PaymentId == id);
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Booking.Models;
using Metro_Ticket_Booking.DTOs.Payments;
using Metro_Ticket_Booking.Services;

namespace Metro_Ticket_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly MetroTicketContext _context;
        private readonly PaymentService _paymentService;

        public PaymentsController(MetroTicketContext context, PaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        // ------------------- Razorpay Integration Endpoints -------------------

        /// <summary>
        /// Create a Razorpay order and return its details for frontend checkout.
        /// </summary>
        [HttpPost("create-order")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequestDto request)
        {
            try
            {
                var amountInPaise = Convert.ToInt32(request.Amount); // amount should be in paise
                var order = _paymentService.CreateOrder(amountInPaise, request.Currency);

                var id = order["id"].ToString();
                var amount = Convert.ToInt32(order["amount"]);
                var currency = order["currency"].ToString();

                return Ok(new { id, amount, currency });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to create Razorpay order", error = ex.Message });
            }
        }


        /// <summary>
        /// Verify the Razorpay payment signature for security.
        /// </summary>
        [HttpPost("verify-signature")]
        public IActionResult VerifySignature([FromBody] VerifySignatureRequestDto request)
        {
            var valid = _paymentService.VerifySignature(request.OrderId, request.PaymentId, request.Signature);

            if (!valid)
                return BadRequest(new { message = "Invalid payment signature" });

            return Ok(new { message = "Payment signature verified" });
        }

        /// <summary>
        /// Save verified payment details and booking info.
        /// </summary>
        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentRequestDto request)
        {
            var payment = new Payment
            {
                TicketId = request.TicketId,
                Amount = request.Amount,
                Currency = request.Currency,
                PaymentDate = request.PaymentDate,
                PaymentStatus = request.PaymentStatus,
                RazorpayOrderId = request.RazorpayOrderId,
                RazorpayPaymentId = request.RazorpayPaymentId,
                RazorpaySignature = request.RazorpaySignature
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Payment recorded successfully" });
        }

        // ------------------- Existing CRUD Endpoints (Optional Admin Use) -------------------

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
                return NotFound();

            return payment;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.PaymentId)
                return BadRequest();

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayment", new { id = payment.PaymentId }, payment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }
}