using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.Models.DTOs.Complaint
{
    public class ComplaintRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(30)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(50)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message is required")]
        [StringLength(200)]
        public string Message { get; set; } = string.Empty;
    }

    public class ComplaintReply
    {
        [Required(ErrorMessage = "Reply message is required")]
        [StringLength(200)]
        public string Message { get; set; } = string.Empty;
    }
}