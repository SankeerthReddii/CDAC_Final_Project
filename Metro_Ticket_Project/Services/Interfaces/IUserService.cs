using Metro_Ticket_Project.Models;
using Metro_Ticket_Project.Models.DTOs.Auth;
using Metro_Ticket_Project.Models.DTOs.User;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> AuthenticateUserAsync(string email, string password);
        Task SignupAsync(RegisterRequest registerRequest);
    }
}