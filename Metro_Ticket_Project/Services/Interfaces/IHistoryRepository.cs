using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IHistoryRepository : IBaseRepository<History>
    {
        Task<IEnumerable<History>> FindByEmailAsync(string email);
        Task<int> GetCountOfTicketsAsync();
        Task<int> GetCountOfRechargeAsync();
    }
}