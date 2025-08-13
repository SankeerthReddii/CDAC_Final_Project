using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IComplaintRepository
    {
        Task<List<Complaint>> GetAllPendingComplaintsAsync();
        Task<List<Complaint>> GetAllComplaintsAsync();
        Task<List<Complaint>> GetComplaintsByEmailAsync(string email);
        Task<Complaint?> GetComplaintByIdAsync(int id);
        Task<Complaint> AddComplaintAsync(Complaint complaint);
        Task<Complaint> UpdateComplaintAsync(Complaint complaint);
        Task<Reply> AddReplyAsync(Reply reply);
    }
}