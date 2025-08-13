using Metro_Ticket_Project.Models.DTOs.Complaint;
using Metro_Ticket_Project.Models.DTOs.User;
using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IComplaintService
    {
        Task<List<Complaint>> GetAllPendingComplaintsAsync();
        Task<List<Complaint>> GetAllComplaintsAsync();
        Task<List<Complaint>> DisplayAllComplaintsAsync();
        Task<List<Complaint>> DisplayAllComplaintsByEmailAsync(UserEmailRequest request);
        Task<Complaint?> RegisterComplaintAsync(ComplaintRequest complaint);
        Task<string> ReplyToComplaintsAsync(int id, ReplyToComplaint msgString);
    }
}