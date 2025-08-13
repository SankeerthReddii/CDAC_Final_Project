using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IMetroCardRepository : IBaseRepository<MetroCard>
    {
        Task<MetroCard?> FindByCardNoAndPinAsync(string cardNo, int pin);
        Task<byte[]?> GetICardByIdAsync(int id);
        Task<MetroCard?> GetByUserIdAsync(int userId);
        Task<IEnumerable<MetroCard>> GetAllPendingCardsAsync();
        Task<int> GetCountOfApprovedCardsAsync();
        Task<int> GetAllPendingCardRequestAsync();
        Task SaveAsync(MetroCard card);
    }
}