//using Metro_Ticket_Booking.DTOs;
//using Metro_Ticket_Booking.Models;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Metro_Ticket_Booking.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly MetroTicketContext _context;

//        public UserService(MetroTicketContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Station>> GetAvailableStationsAsync()
//        {
//            return await _context.Stations.OrderBy(s => s.Name).ToListAsync();
//        }

//        public async Task<IEnumerable<Models.Route>> GetAvailableRoutesAsync()
//        {
//            return await _context.Routes
//                .Include(r => r.StartStation)
//                .Include(r => r.EndStation)
//                .OrderBy(r => r.Name)
//                .ToListAsync();
//        }

//        public async Task<Ticket> BookTicketAsync(TicketBookingDto bookingDto)
//        {
//            var ticket = new Ticket
//            {
//                UserId = bookingDto.UserId,
//                RouteId = bookingDto.RouteId,
//                MetroId = bookingDto.MetroId,
//                FromStationId = bookingDto.FromStationId,
//                ToStationId = bookingDto.ToStationId,
//                Price = bookingDto.Price,
//                BookingDate = System.DateTime.UtcNow,
//                //TravelDate = bookingDto.TravelDate
//            };

//            _context.Tickets.Add(ticket);
//            await _context.SaveChangesAsync();

//            return ticket;
//        }

//        public async Task<bool> ApplyMetroCardToBookingAsync(int bookingId, int metroCardId)
//        {
//            var ticket = await _context.Tickets.FindAsync(bookingId);
//            var metroCard = await _context.MetroCards.FindAsync(metroCardId);

//            if (ticket == null || metroCard == null || metroCard.CardStatus != "Approved")
//                return false;

//            ticket.MetroId = metroCardId;
//            await _context.SaveChangesAsync();

//            return true;
//        }

//        public async Task<Complaint> RaiseComplaintAsync(ComplaintCreateDto complaintDto)
//        {
//            var complaint = new Complaint
//            {
//                UserId = complaintDto.UserId,
//                ComplaintId = complaintDto.BookingId,
//                Message = complaintDto.Description,
//                Status = "Pending",
//                SubmittedAt = System.DateTime.UtcNow
//            };

//            _context.Complaints.Add(complaint);
//            await _context.SaveChangesAsync();

//            return complaint;
//        }

//        public async Task<IEnumerable<Complaint>> GetComplaintsByUserAsync(string userId)
//        {
//            if (!int.TryParse(userId, out int uid))
//                return new List<Complaint>();

//            return await _context.Complaints
//                .Where(c => c.UserId == uid)
//                .OrderByDescending(c => c.SubmittedAt)
//                .ToListAsync();
//        }

//        public async Task<IEnumerable<Ticket>> GetBookingsByUserAsync(string userId)
//        {
//            if (!int.TryParse(userId, out int uid))
//                return new List<Ticket>();

//            return await _context.Tickets
//                .Include(t => t.Route)
//                .Include(t => t.Metro)
//                .Include(t => t.FromStation)
//                .Include(t => t.ToStation)
//                .Where(t => t.UserId == uid)
//                .OrderByDescending(t => t.BookingDate)
//                .ToListAsync();
//        }

//        public async Task<bool> RechargeMetroCardAsync(int metroCardId, int amount)
//        {
//            var metroCard = await _context.MetroCards.FindAsync(metroCardId);
//            if (metroCard == null || metroCard.CardStatus != "Approved")
//                return false;

//            if (metroCard.Balance == null)
//                metroCard.Balance = 0;

//            metroCard.Balance += amount;
//            await _context.SaveChangesAsync();

//            return true;
//        }

//        // ✅ Removed unnecessary duplicate overload with decimal
//    }
//}


using Metro_Ticket_Booking.DTOs;
using Metro_Ticket_Booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metro_Ticket_Booking.Services
{
    public class UserService : IUserService
    {
        private readonly MetroTicketContext _context;

        public UserService(MetroTicketContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Station>> GetAvailableStationsAsync()
        {
            return await _context.Stations.OrderBy(s => s.Name).ToListAsync();
        }

        public async Task<IEnumerable<Models.Route>> GetAvailableRoutesAsync()
        {
            return await _context.Routes
                .Include(r => r.StartStation)
                .Include(r => r.EndStation)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        public async Task<Ticket> BookTicketAsync(TicketBookingDto bookingDto)
        {
            var ticket = new Ticket
            {
                UserId = bookingDto.UserId,
                RouteId = bookingDto.RouteId,
                MetroId = bookingDto.MetroId,
                FromStationId = bookingDto.FromStationId,
                ToStationId = bookingDto.ToStationId,
                Price = bookingDto.Price,
                TicketCount = bookingDto.TicketCount,
                TravelDate = bookingDto.TravelDate,
                BookingDate = System.DateTime.UtcNow
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        // In UserService:
        public async Task<MetroCard?> ApplyMetroCardToBookingAsync(int userId, string nameOnCard, string cardType)
        {
            // Check if user already has a metro card
            var existingCard = await _context.MetroCards
                .FirstOrDefaultAsync(mc => mc.UserId == userId);

            if (existingCard != null)
            {
                // Optionally, update existing card details or just return existing
                return existingCard;
            }

            var newCard = new MetroCard
            {
                UserId = userId,
                NameOnCard = nameOnCard,
                CardType = cardType,
                CardStatus = "Pending",
                ApplicationDate = DateTime.UtcNow,
                Balance = 100
            };

            _context.MetroCards.Add(newCard);
            await _context.SaveChangesAsync();

            return newCard;
        }




        public async Task<Complaint> RaiseComplaintAsync(ComplaintCreateDto complaintDto)
        {
            var complaint = new Complaint
            {
                UserId = complaintDto.UserId,
                ComplaintId = complaintDto.BookingId,
                Message = complaintDto.Description,
                Status = "Pending",
                SubmittedAt = System.DateTime.UtcNow
            };

            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();

            return complaint;
        }

        public async Task<IEnumerable<Complaint>> GetComplaintsByUserAsync(string userId)
        {
            if (!int.TryParse(userId, out int uid))
                return new List<Complaint>();

            return await _context.Complaints
                .Where(c => c.UserId == uid)
                .OrderByDescending(c => c.SubmittedAt)
                .ToListAsync();
        }

        //public async Task<IEnumerable<Ticket>> GetBookingsByUserAsync(string userId)
        //{
        //    if (!int.TryParse(userId, out int uid))
        //        return new List<Ticket>();

        //    return await _context.Tickets
        //        .Include(t => t.Route)
        //        .Include(t => t.Metro)
        //        .Include(t => t.FromStation)
        //        .Include(t => t.ToStation)
        //        .Where(t => t.UserId == uid)
        //        .OrderByDescending(t => t.BookingDate)
        //        .ToListAsync();
        //}

        //public async Task<MetroCard> GetMetroCardByUserIdAsync(int userId)
        //{

        //}

         async Task<MetroCard> IUserService.GetMetroCardByUserIdAsync(int userId)
        {
            return await _context.MetroCards.FirstOrDefaultAsync(mc => mc.UserId == userId);
        }

        public async Task<bool> RechargeMetroCardAsync(int metroCardId, int amount)
        {
            var metroCard = await _context.MetroCards.FindAsync(metroCardId);
            if (metroCard == null || metroCard.CardStatus != "Approved")
                return false;

            if (metroCard.Balance == null)
                metroCard.Balance = 0;

            metroCard.Balance += amount;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<BookingHistoryDto>> GetBookingsByUserAsync(string userId)
        {
            if (!int.TryParse(userId, out int uid))
                return new List<BookingHistoryDto>();

            // Make sure TravelDate column exists in DB, else you can return null/fallback here
            return await _context.Tickets
                .Include(t => t.FromStation)
                .Include(t => t.ToStation)
                .Where(t => t.UserId == uid)
                .OrderByDescending(t => t.BookingDate)
                .Select(t => new BookingHistoryDto
                {
                    Id = t.TicketId,
                    FromStation = t.FromStation.Name,
                    ToStation = t.ToStation.Name,
                    BookingDate = t.BookingDate,
                    //TravelDate = t.TravelDate,       // assumes nullable DateTime? - add column if missing in DB
                    //TravelTime = null,                // fill appropriately if you have a field for travel time
                    NumberOfTickets = t.TicketCount,
                    TotalAmount = t.Price,
                    Status = "Booked"                 // replace with actual status if stored
                })
                .ToListAsync();
        }

        // In UserService.cs
        public async Task<int> GetTotalBookingsByUserAsync(int userId) //GetTotalBookingsByUserAsync
        {
            // Sum the TicketCount for this user
            return await _context.Tickets
                .Where(t => t.UserId == userId)
                .SumAsync(t => (int?)t.TicketCount ?? 0); // handle null ticket counts
        }


        // ✅ Removed unnecessary duplicate overload with decimal
    }
}
