using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Project.Infrastructure.Data;
using Metro_Ticket_Project.Models.Entities;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Repositories
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly MetroDbContext _context;

        public ComplaintRepository(MetroDbContext context)
        {
            _context = context;
        }

        public async Task<List<Complaint>> GetAllPendingComplaintsAsync()
        {
            return await _context.Complaints
                .Where(c => c.Status == "Pending")
                .Include(c => c.User)
                .OrderByDescending(c => c.DateTime)
                .ToListAsync();
        }

        public async Task<List<Complaint>> GetAllComplaintsAsync()
        {
            return await _context.Complaints
                .Include(c => c.User)
                .Include(c => c.Replies)
                .OrderByDescending(c => c.Status)
                .ToListAsync();
        }

        public async Task<List<Complaint>> GetComplaintsByEmailAsync(string email)
        {
            return await _context.Complaints
                .Include(c => c.User)
                .Include(c => c.Replies)
                .Where(c => c.User.Email == email)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<Complaint?> GetComplaintByIdAsync(int id)
        {
            return await _context.Complaints
                .Include(c => c.User)
                .Include(c => c.Replies)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Complaint> AddComplaintAsync(Complaint complaint)
        {
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }

        public async Task<Complaint> UpdateComplaintAsync(Complaint complaint)
        {
            _context.Complaints.Update(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }

        public async Task<Reply> AddReplyAsync(Reply reply)
        {
            _context.Replies.Add(reply);
            await _context.SaveChangesAsync();
            return reply;
        }
    }
}