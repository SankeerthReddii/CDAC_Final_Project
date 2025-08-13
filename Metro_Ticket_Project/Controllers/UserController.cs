using Metro_Ticket_Project.Models.DTOs.Auth;
using Metro_Ticket_Project.Models.DTOs.User;
using Metro_Ticket_Project.Services.Interfaces;
using Metro_Ticket_Project.Services.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly SendMail _mailService;

        public UserController(IUserService userService, SendMail mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        // User sign in method
        [HttpPost("sign_in")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
        {
            var result = await _userService.AuthenticateUserAsync(request.Email, request.Password);
            return Ok(result);
        }

        // User sign up method
        [HttpPost("sign_up")]
        public async Task<IActionResult> SignUp([FromBody] RegisterRequest registerRequest)
        {
            await _userService.SignupAsync(registerRequest);

            // Uncomment when mail service is implemented
            // await _mailService.SendMailAsync(new NotificationEmail 
            // { 
            //     Subject = "Sign Up", 
            //     To = registerRequest.Email, 
            //     Body = "You have successfully created account with us!!!\nClick here to login with your account- \nhttps://localhost:8080/sign_in" 
            // });

            return Ok("User Registered Successfully!!!");
        }
    }
}