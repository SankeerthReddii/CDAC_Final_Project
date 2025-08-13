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
//    public class MetroCardsController : ControllerBase
//    {
//        private readonly MetroTicketContext _context;

//        public MetroCardsController(MetroTicketContext context)
//        {
//            _context = context;
//        }

//        // GET: api/MetroCards
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<MetroCard>>> GetMetroCards()
//        {
//            return await _context.MetroCards.ToListAsync();
//        }

//        // GET: api/MetroCards/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<MetroCard>> GetMetroCard(int id)
//        {
//            var metroCard = await _context.MetroCards.FindAsync(id);

//            if (metroCard == null)
//            {
//                return NotFound();
//            }

//            return metroCard;
//        }

//        // PUT: api/MetroCards/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutMetroCard(int id, MetroCard metroCard)
//        {
//            if (id != metroCard.CardId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(metroCard).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!MetroCardExists(id))
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

//        // POST: api/MetroCards
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<MetroCard>> PostMetroCard(MetroCard metroCard)
//        {
//            _context.MetroCards.Add(metroCard);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetMetroCard", new { id = metroCard.CardId }, metroCard);
//        }

//        // DELETE: api/MetroCards/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteMetroCard(int id)
//        {
//            var metroCard = await _context.MetroCards.FindAsync(id);
//            if (metroCard == null)
//            {
//                return NotFound();
//            }

//            _context.MetroCards.Remove(metroCard);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool MetroCardExists(int id)
//        {
//            return _context.MetroCards.Any(e => e.CardId == id);
//        }
//    }
//}
