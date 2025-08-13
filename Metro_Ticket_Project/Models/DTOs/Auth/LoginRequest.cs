using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.Models.DTOs.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;
    }

    //public class LoginResponse
    //{
    //    public string Email { get; set; } = string.Empty;
    //    public string Token { get; set; } = string.Empty; // Added for JWT token
    //}
}