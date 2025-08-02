using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface ITripDetailsRepository : IBaseRepository<TripDetails>
    {
        Task<IEnumerable<TripDetails>> GetScheduleByIdAsync(int stationId);
    }
}