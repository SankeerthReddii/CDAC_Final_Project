using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.DTO
{
    public class MetroCardRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        public byte[]? Photo { get; set; }
    }
}