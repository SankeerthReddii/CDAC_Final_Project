//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Metro_Ticket_Booking.Models;
//using Metro_Ticket_Booking.DTOs.Stations;
//using Microsoft.AspNetCore.Authorization;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Metro_Ticket_Booking.Controllers
//{
//    [Route("api/admin/stations")]
//    [ApiController]
//    [Authorize(Roles = "admin")]
//    public class AdminStationsController : ControllerBase
//    {
//        private readonly MetroTicketContext _context;

//        public AdminStationsController(MetroTicketContext context)
//        {
//            _context = context;
//        }

//        // GET all stations (clean DTO output)
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<StationReadDto>>> GetStations()
//        {
//            var stations = await _context.Stations
//                .Select(s => new StationReadDto
//                {
//                    StationId = s.StationId,
//                    Name = s.Name,
//                    Address = s.Address
//                })
//                .ToListAsync();

//            return Ok(stations);
//        }

//        // GET single station
//        [HttpGet("{id}")]
//        public async Task<ActionResult<StationReadDto>> GetStation(int id)
//        {
//            var station = await _context.Stations
//                .Where(s => s.StationId == id)
//                .Select(s => new StationReadDto
//                {
//                    StationId = s.StationId,
//                    Name = s.Name,
//                    Address = s.Address
//                })
//                .FirstOrDefaultAsync();

//            if (station == null) return NotFound();
//            return Ok(station);
//        }

//        // POST new station (DTO input)
//        [HttpPost]
//        public async Task<ActionResult<StationReadDto>> PostStation(StationCreateDto dto)
//        {
//            if (string.IsNullOrWhiteSpace(dto.Name))
//                return BadRequest("Station name is required.");

//            // Check if station already exists
//            bool exists = await _context.Stations
//                .AnyAsync(s => s.Name.ToLower() == dto.Name.Trim().ToLower());

//            if (exists)
//                return Conflict($"Station '{dto.Name}' already exists.");

//            var station = new Station
//            {
//                Name = dto.Name.Trim(),
//                Address = string.IsNullOrWhiteSpace(dto.Address) ? null : dto.Address.Trim()
//            };

//            _context.Stations.Add(station);
//            await _context.SaveChangesAsync();

//            // Return a clean DTO
//            var readDto = new StationReadDto
//            {
//                StationId = station.StationId,
//                Name = station.Name,
//                Address = station.Address
//            };

//            return CreatedAtAction(nameof(GetStation), new { id = station.StationId }, readDto);
//        }

//        // PUT update station (DTO input)
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutStation(int id, StationUpdateDto dto)
//        {
//            var station = await _context.Stations.FindAsync(id);
//            if (station == null) return NotFound();

//            if (!string.IsNullOrWhiteSpace(dto.Name))
//                station.Name = dto.Name.Trim();
//            if (!string.IsNullOrWhiteSpace(dto.Address))
//                station.Address = dto.Address.Trim();

//            _context.Entry(station).State = EntityState.Modified;
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        // DELETE station
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteStation(int id)
//        {
//            var station = await _context.Stations.FindAsync(id);
//            if (station == null) return NotFound();

//            _context.Stations.Remove(station);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}
