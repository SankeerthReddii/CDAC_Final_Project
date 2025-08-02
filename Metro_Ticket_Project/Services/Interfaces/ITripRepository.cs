using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface ITripRepository : IBaseRepository<Trip>
    {
        Task<Trip?> GetTrainNoByTripIdAsync(int tripId);
        Task<string?> GetTowardsStationNameAsync(int tripId);
    }
}