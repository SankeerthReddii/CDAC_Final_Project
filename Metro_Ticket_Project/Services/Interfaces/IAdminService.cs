using Metro_Ticket_Project.Models;
using Metro_Ticket_Project.Models.DTOs.Auth;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IAdminService
    {
        Task<LoginResponse> AdminLogInAsync(string email, string password);
    }
}