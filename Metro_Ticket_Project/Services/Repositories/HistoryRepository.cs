using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Project.Models.Entities;
using Metro_Ticket_Project.Infrastructure.Data;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Repositories
{
    public class HistoryRepository : BaseRepository<History>, IHistoryRepository
    {
        public HistoryRepository(MetroDbContext context) : base(context) { }

        public async Task<IEnumerable<History>> FindByEmailAsync(string email)
        {
            return await _dbSet
                .Where(h => h.Email == email)
                .ToListAsync();
        }

        public async Task<int> GetCountOfTicketsAsync()
        {
            return await _dbSet
                .CountAsync(h => h.TransactionType == "Ticket Booking");
        }

        public async Task<int> GetCountOfRechargeAsync()
        {
            return await _dbSet
                .CountAsync(h => h.TransactionType == "Card Recharge");
        }
    }
}