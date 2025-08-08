using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Booking.DTOs;
public class LoginUserDto
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
}