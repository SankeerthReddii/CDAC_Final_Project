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
//    public class MetroesController : ControllerBase
//    {
//        private readonly MetroTicketContext _context;

//        public MetroesController(MetroTicketContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Metroes
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Metro>>> GetMetros()
//        {
//            return await _context.Metros.ToListAsync();
//        }

//        // GET: api/Metroes/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Metro>> GetMetro(int id)
//        {
//            var metro = await _context.Metros.FindAsync(id);

//            if (metro == null)
//            {
//                return NotFound();
//            }

//            return metro;
//        }

//        // PUT: api/Metroes/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutMetro(int id, Metro metro)
//        {
//            if (id != metro.MetroId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(metro).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!MetroExists(id))
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

//        // POST: api/Metroes
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Metro>> PostMetro(Metro metro)
//        {
//            _context.Metros.Add(metro);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetMetro", new { id = metro.MetroId }, metro);
//        }

//        // DELETE: api/Metroes/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteMetro(int id)
//        {
//            var metro = await _context.Metros.FindAsync(id);
//            if (metro == null)
//            {
//                return NotFound();
//            }

//            _context.Metros.Remove(metro);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool MetroExists(int id)
//        {
//            return _context.Metros.Any(e => e.MetroId == id);
//        }
//    }
//}
