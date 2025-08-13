// DTOs/RegisterRequest.cs
using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.DTO
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }
    }
}