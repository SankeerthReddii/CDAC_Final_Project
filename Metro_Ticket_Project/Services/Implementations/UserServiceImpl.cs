using Metro_Ticket_Project.Exceptions;
using Metro_Ticket_Project.Models;
using Metro_Ticket_Project.Models.DTOs.Auth;
using Metro_Ticket_Project.Models.DTOs.User;
using Metro_Ticket_Project.Models.Entities;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Implementations
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserServiceImpl(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<LoginResponse> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepo.FindByEmailAndPasswordAsync(email, password);
            if (user == null)
            {
                throw new CustomExceptionHandler("Authentication failed...");
            }
            return new LoginResponse(user.Email);
        }

        public async Task SignupAsync(RegisterRequest registerRequest)
        {
            var user = new User
            {
                Email = registerRequest.Email,
                Password = registerRequest.Password,
                DateOfBirth = registerRequest.DateOfBirth,
                Gender = registerRequest.Gender,
                Name = registerRequest.Name,
                Phone = registerRequest.Phone,
                Address = registerRequest.Address
            };

            await _userRepo.SaveAsync(user);
        }

        //public Task SignupAsync(RegisterRequest registerRequest)
        //{
        //    Console.WriteLine(registerRequest.Email);
        //    throw new NotImplementedException();
        //}
    }
}