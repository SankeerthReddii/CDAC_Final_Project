using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IStationRepository : IBaseRepository<Station>
    {
        Task<int> GetStationIdAsync(string name);
        Task<Station?> GetStationByIdAsync(int id);
    }
}