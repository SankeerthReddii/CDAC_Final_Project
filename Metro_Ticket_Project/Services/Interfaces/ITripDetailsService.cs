using Metro_Ticket_Project.Models;
using Metro_Ticket_Project.Models.DTOs.Schedule;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface ITripDetailsService
    {
        Task<List<ScheduleResponse>> GetScheduleAsync(int id);
    }
}