using Metro_Ticket_Project.Models.DTOs.Complaint;
using Metro_Ticket_Project.Models.DTOs.User;
using Metro_Ticket_Project.Models.Entities;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Implementations
{
    public class ComplaintServiceImpl : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IUserRepository _userRepository;

        public ComplaintServiceImpl(IComplaintRepository complaintRepository, IUserRepository userRepository)
        {
            _complaintRepository = complaintRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Complaint>> GetAllPendingComplaintsAsync()
        {
            return await _complaintRepository.GetAllPendingComplaintsAsync();
        }

        public async Task<List<Complaint>> GetAllComplaintsAsync()
        {
            return await _complaintRepository.GetAllComplaintsAsync();
        }

        public async Task<List<Complaint>> DisplayAllComplaintsAsync()
        {
            return await _complaintRepository.GetAllComplaintsAsync();
        }

        public async Task<List<Complaint>> DisplayAllComplaintsByEmailAsync(UserEmailRequest request)
        {
            return await _complaintRepository.GetComplaintsByEmailAsync(request.Email);
        }

        public async Task<Complaint?> RegisterComplaintAsync(ComplaintRequest complaint)
        {
            try
            {
                var user = await _userRepository.FindByEmailAsync(complaint.Email);
                if (user == null)
                {
                    return null;
                }

                var newComplaint = new Complaint
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Address = user.Address,
                    Phone = user.Phone,
                    Email = user.Email,
                    Message = complaint.Message,
                    Status = "Pending",
                    DateTime = DateTime.Now
                };

                return await _complaintRepository.AddComplaintAsync(newComplaint);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering complaint: {ex.Message}");
                return null;
            }
        }

        public async Task<string> ReplyToComplaintsAsync(int id, ReplyToComplaint msgString)
        {
            try
            {
                var complaint = await _complaintRepository.GetComplaintByIdAsync(id);
                if (complaint == null)
                {
                    return "Complaint not found";
                }

                var reply = new Reply
                {
                    ComplaintId = id,
                    Complaint = complaint,
                    ReplyText = msgString.MessageString,
                    ReplyDate = DateTime.Now,
                    RepliedBy = "Admin"
                };

                await _complaintRepository.AddReplyAsync(reply);

                complaint.Status = "Resolved";
                complaint.Response = msgString.MessageString;
                complaint.DateTime = DateTime.Now;

                await _complaintRepository.UpdateComplaintAsync(complaint);

                return "Reply sent successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error replying to complaint: {ex.Message}");
                return "Failed to send reply";
            }
        }
    }
}
