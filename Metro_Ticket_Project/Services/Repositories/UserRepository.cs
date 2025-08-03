using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Project.Models.Entities;
using Metro_Ticket_Project.Infrastructure.Data;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MetroDbContext context) : base(context) { }

        public async Task<User?> FindByEmailAndPasswordAsync(string email, string password)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task SaveAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}