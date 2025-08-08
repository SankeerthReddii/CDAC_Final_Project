using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Booking.DTOs;
public class RegisterUserDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Date of Birth is required.")]
    [DataType(DataType.Date)]
    public DateOnly Dob { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(250, ErrorMessage = "Address can't be longer than 250 characters.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Gender is required.")]
    [RegularExpression("male|female|other", ErrorMessage = "Gender must be 'male', 'female' or 'other'.")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    [StringLength(150)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be minimum 6 characters.")]
    public string Password { get; set; }
}
