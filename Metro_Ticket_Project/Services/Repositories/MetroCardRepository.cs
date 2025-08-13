using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Project.Models.Entities;
using Metro_Ticket_Project.Infrastructure.Data;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Repositories
{
    public class MetroCardRepository : BaseRepository<MetroCard>, IMetroCardRepository
    {
        public MetroCardRepository(MetroDbContext context) : base(context) { }

        public async Task<MetroCard?> FindByCardNoAndPinAsync(string cardNo, int pin)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.CardNo == cardNo && c.Pin == pin);
        }

        public async Task<byte[]?> GetICardByIdAsync(int id)
        {
            var card = await _dbSet.FindAsync(id);
            return card?.ICard;
        }

        public async Task<MetroCard?> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<IEnumerable<MetroCard>> GetAllPendingCardsAsync()
        {
            return await _dbSet
                .Where(c => c.CardStatus == false)
                .ToListAsync();
        }

        public async Task<int> GetCountOfApprovedCardsAsync()
        {
            return await _dbSet
                .CountAsync(c => c.CardStatus == true);
        }

        public async Task<int> GetAllPendingCardRequestAsync()
        {
            return await _dbSet
                .CountAsync(c => c.CardStatus == false);
        }

        public Task SaveAsync(MetroCard card)
        {
            throw new NotImplementedException();
        }
    }
}