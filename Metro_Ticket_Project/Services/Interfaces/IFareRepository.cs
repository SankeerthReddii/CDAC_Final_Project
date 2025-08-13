using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IFareRepository : IBaseRepository<Fare>
    {
        Task<decimal> GetFareAmountAsync(int source, int destination);
        Task<IEnumerable<Fare>> GetFareFromStationAsync(int source);
    }
}