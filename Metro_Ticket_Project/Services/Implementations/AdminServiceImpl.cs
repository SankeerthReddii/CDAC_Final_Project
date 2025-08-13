//using Metro_Ticket_Project.Data.Interfaces;
using Metro_Ticket_Project.Exceptions;
using Metro_Ticket_Project.Models;
using Metro_Ticket_Project.Models.DTOs.Auth;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Implementations
{
    public class AdminServiceImpl : IAdminService
    {
        private readonly IAdminRepository _adminRepo;

        public AdminServiceImpl(IAdminRepository adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task<LoginResponse> AdminLogInAsync(string email, string password)
        {
            var admin = await _adminRepo.FindByEmailAndPasswordAsync(email, password);
            if (admin == null)
            {
                throw new CustomExceptionHandler("Authentication failed...");
            }
            return new LoginResponse(email);
        }
    }
}