using Metro_Ticket_Booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metro_Ticket_Booking.Controllers
{
    [ApiController]
    [Route("api/Payment")]
    public class PaymentController : Controller
    {
        private readonly MetroTicketContext _context;

        public PaymentController(MetroTicketContext context)
        {
            _context = context;
        }

        //[HttpPost]

        [HttpPost("confirm/payment")]
        public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
        {
            if (!_context.Tickets.Any(t => t.TicketId == payment.TicketId))
            {
                return BadRequest("Invalid TicketId.");
            }

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return Ok(payment);
        }

        [HttpGet("ticket/{ticketId}")]
        public async Task<IActionResult> GetPaymentByTicketId(int ticketId)
        {
            var payment = await _context.Payments
                .Where(p => p.TicketId == ticketId)
                .FirstOrDefaultAsync();

            if (payment == null) return NotFound();
            return Ok(payment);
        }
    }
}


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
