using Metro_Ticket_Booking.DTOs;
using Metro_Ticket_Booking.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metro_Ticket_Booking.Services
{
    public class AdminService : IAdminService
    {
        private readonly MetroTicketContext _context;

        public AdminService(MetroTicketContext context)
        {
            _context = context;
        }

        // ---------------------- Dashboard ----------------------

        public async Task<AdminDashboardMetricsDto> GetDashboardMetricsAsync()
        {
            var metrics = new AdminDashboardMetricsDto
            {
                TotalUsers = await _context.Users.CountAsync(),
                TotalRevenue = await _context.Payments
                                   .Where(p => p.PaymentStatus == "Completed")
                                   .SumAsync(p => p.Amount),
                TotalBookings = await _context.Tickets.CountAsync(),
                ActiveStations = await _context.Stations.CountAsync() // Optionally filter active stations
            };

            return metrics;
        }

        // ---------------------- Stations CRUD ----------------------

        public async Task<IEnumerable<Station>> GetStationsAsync()
        {
            return await _context.Stations.OrderBy(s => s.Name).ToListAsync();
        }

        public async Task<Station> GetStationByIdAsync(int id)
        {
            return await _context.Stations.FindAsync(id);
        }

        public async Task<Station> CreateStationAsync(Station station)
        {
            _context.Stations.Add(station);
            await _context.SaveChangesAsync();
            return station;
        }

        public async Task<Station> UpdateStationAsync(int id, Station station)
        {
            var existingStation = await _context.Stations.FindAsync(id);
            if (existingStation == null)
                return null;

            existingStation.Name = station.Name;
            existingStation.Address = station.Address;
            // Add more fields if needed

            await _context.SaveChangesAsync();
            return existingStation;
        }

        public async Task<bool> DeleteStationAsync(int id)
        {
            var station = await _context.Stations.FindAsync(id);
            if (station == null)
                return false;

            _context.Stations.Remove(station);
            await _context.SaveChangesAsync();
            return true;
        }

        // ---------------------- Metros CRUD ----------------------

        public async Task<IEnumerable<Metro>> GetMetrosAsync()
        {
            return await _context.Metros.OrderBy(m => m.Name).ToListAsync();
        }

        public async Task<Metro> GetMetroByIdAsync(int id)
        {
            return await _context.Metros.FindAsync(id);
        }

        public async Task<Metro> CreateMetroAsync(Metro metro)
        {
            _context.Metros.Add(metro);
            await _context.SaveChangesAsync();
            return metro;
        }

        public async Task<Metro> UpdateMetroAsync(int id, Metro metro)
        {
            var existingMetro = await _context.Metros.FindAsync(id);
            if (existingMetro == null)
                return null;

            existingMetro.Name = metro.Name;
            // Add more fields if required

            await _context.SaveChangesAsync();
            return existingMetro;
        }

        public async Task<bool> DeleteMetroAsync(int id)
        {
            var metro = await _context.Metros.FindAsync(id);
            if (metro == null)
                return false;

            _context.Metros.Remove(metro);
            await _context.SaveChangesAsync();
            return true;
        }

        // ---------------------- Routes CRUD ----------------------

        //public async Task<IEnumerable<Models.Route>> GetRoutesAsync()
        //{
        //    return await _context.Routes
        //        .Include(r => r.StartStation)
        //        .Include(r => r.EndStation)
        //        .OrderBy(r => r.Name)
        //        .ToListAsync();
        //}

        //public async Task<Models.Route> GetRouteByIdAsync(int id)
        //{
        //    return await _context.Routes
        //        .Include(r => r.StartStation)
        //        .Include(r => r.EndStation)
        //        .FirstOrDefaultAsync(r => r.RouteId == id);
        //}

        public async Task<IEnumerable<Models.Route>> GetRoutesAsync()
        {
            return await _context.Routes
                .Include(r => r.StartStation)
                .Include(r => r.EndStation)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        public async Task<Models.Route> GetRouteByIdAsync(int id)
        {
            return await _context.Routes
                .Include(r => r.StartStation)
                .Include(r => r.EndStation)
                .FirstOrDefaultAsync(r => r.RouteId == id);
        }



        public async Task<Models.Route> CreateRouteAsync(Models.Route route)
        {
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();
            return route;
        }

        public async Task<Models.Route> UpdateRouteAsync(int id, Models.Route route)
        {
            var existingRoute = await _context.Routes.FindAsync(id);
            if (existingRoute == null)
                return null;

            existingRoute.Name = route.Name;
            existingRoute.StartStationId = route.StartStationId;
            existingRoute.EndStationId = route.EndStationId;

            await _context.SaveChangesAsync();
            return existingRoute;
        }

        public async Task<bool> DeleteRouteAsync(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null)
                return false;

            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return true;
        }

        // ---------------------- Complaints Management ----------------------

        public async Task<IEnumerable<Complaint>> GetPendingComplaintsAsync()
        {
            return await _context.Complaints
                .Include(c => c.User)
                .Where(c => c.Status == "Pending")
                .OrderBy(c => c.SubmittedAt)
                .ToListAsync();
        }

        public async Task<bool> UpdateComplaintStatusAsync(int complaintId, string newStatus)
        {
            var complaint = await _context.Complaints.FindAsync(complaintId);
            if (complaint == null)
                return false;

            complaint.Status = newStatus;

            if (newStatus.ToLower() == "resolved" || newStatus.ToLower() == "closed")
                complaint.RepliedAt = System.DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        // ---------------------- Metrocard Approvals ----------------------

        public async Task<IEnumerable<MetroCard>> GetPendingMetroCardApplicationsAsync()
        {
            return await _context.MetroCards
                .Include(m => m.User)
                .Where(m => m.CardStatus == "Pending")
                .OrderBy(m => m.ApplicationDate)
                .ToListAsync();
        }

        public async Task<bool> ApproveMetroCardAsync(int cardId)
        {
            var card = await _context.MetroCards.FindAsync(cardId);
            if (card == null || card.CardStatus != "Pending")
                return false;

            card.CardStatus = "Approved";
            card.ApprovedDate = System.DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectMetroCardAsync(int cardId)
        {
            var card = await _context.MetroCards.FindAsync(cardId);
            if (card == null || card.CardStatus != "Pending")
                return false;

            card.CardStatus = "Rejected";
            // Optional: add reason/message

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Complaint> GetComplaintByIdAsync(int id)
        {
            return await _context.Complaints
                .Include(c => c.User) // load user if needed for email, etc
                .FirstOrDefaultAsync(c => c.ComplaintId == id);
        }

        public async Task<Complaint> UpdateComplaintAsync(Complaint complaint)
        {
            var existing = await _context.Complaints.FindAsync(complaint.ComplaintId);
            if (existing == null)
                return null;

            existing.Reply = complaint.Reply;
            existing.RepliedAt = complaint.RepliedAt;
            existing.Status = complaint.Status; // optionally update status, if your flow requires

            await _context.SaveChangesAsync();
            return existing;
        }

    }
}