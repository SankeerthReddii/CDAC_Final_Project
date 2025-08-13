using Metro_Ticket_Project.Models;
using Metro_Ticket_Project.Models.DTOs.Fare;
using Metro_Ticket_Project.Models.DTOs.Ticket;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IBookingService
    {
        Task<TicketResponse> BookTicketAsync(TicketRequest request);
        Task<List<FareResponse>> GetFairFromStationAsync(int id);
    }
}