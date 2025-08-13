using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Project.Models.Entities;
using Metro_Ticket_Project.Infrastructure.Data;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(MetroDbContext context) : base(context) { }

        public async Task<Admin?> FindByEmailAndPasswordAsync(string email, string password)
        {
            return await _dbSet
                .FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }
    }
}