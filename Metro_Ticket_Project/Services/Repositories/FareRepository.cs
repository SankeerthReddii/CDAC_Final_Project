using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Project.Models.Entities;
using Metro_Ticket_Project.Infrastructure.Data;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Repositories
{
    public class FareRepository : BaseRepository<Fare>, IFareRepository
    {
        public FareRepository(MetroTicketDbContext context) : base(context) { }

        public async Task<decimal> GetFareAmountAsync(int source, int destination)
        {
            var fare = await _dbSet
                .FirstOrDefaultAsync(f => f.Source == source && f.Destination == destination);
            return fare?.Amount ?? 0;
        }

        public async Task<IEnumerable<Fare>> GetFareFromStationAsync(int source)
        {
            return await _dbSet
                .Where(f => f.Source == source)
                .ToListAsync();
        }
    }
}