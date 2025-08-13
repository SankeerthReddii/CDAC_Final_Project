using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.Models.DTOs.User
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [StringLength(20)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        [StringLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(50)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Gender must be Male, Female, or Other")]
        public string Gender { get; set; } = string.Empty;
    }

    public class UserEmailRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
    }
}