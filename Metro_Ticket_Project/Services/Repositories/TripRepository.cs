using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Project.Models.Entities;
using Metro_Ticket_Project.Infrastructure.Data;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Repositories
{
    public class TripRepository : BaseRepository<Trip>, ITripRepository
    {
        public TripRepository(MetroDbContext context) : base(context) { }

        public async Task<Trip?> GetTrainNoByTripIdAsync(int tripId)
        {
            return await _dbSet
                .Include(t => t.Train)
                .FirstOrDefaultAsync(t => t.Id == tripId);
        }

        public async Task<string?> GetTowardsStationNameAsync(int tripId)
        {
            var trip = await _dbSet
                .FirstOrDefaultAsync(t => t.Id == tripId);
            return trip?.End;
        }
    }
}