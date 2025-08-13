using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.DTO
{
    public class UserEmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}