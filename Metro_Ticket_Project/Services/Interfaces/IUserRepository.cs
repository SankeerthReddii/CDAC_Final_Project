using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> FindByEmailAndPasswordAsync(string email, string password);
        Task<User?> FindByEmailAsync(string email);
        Task SaveAsync(User user);
    }
}