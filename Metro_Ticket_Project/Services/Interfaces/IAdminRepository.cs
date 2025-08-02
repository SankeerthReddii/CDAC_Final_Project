using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        Task<Admin?> FindByEmailAndPasswordAsync(string email, string password);
    }
}