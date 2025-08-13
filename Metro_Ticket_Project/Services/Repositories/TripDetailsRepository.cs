using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Project.Models.Entities;
using Metro_Ticket_Project.Infrastructure.Data;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Repositories
{
    public class TripDetailsRepository : BaseRepository<TripDetails>, ITripDetailsRepository
    {
        public TripDetailsRepository(MetroDbContext context) : base(context) { }

        public async Task<IEnumerable<TripDetails>> GetScheduleByIdAsync(int stationId)
        {
            return await _dbSet
                .Where(td => td.StationId == stationId)
                .ToListAsync();
        }
    }
}