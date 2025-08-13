using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.DTO
{
    public class RegisterComplaint
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? Name { get; internal set; }
        public string? Address { get; internal set; }
        public object? Msg { get; internal set; }
        public string? Phone { get; internal set; }
    }
}