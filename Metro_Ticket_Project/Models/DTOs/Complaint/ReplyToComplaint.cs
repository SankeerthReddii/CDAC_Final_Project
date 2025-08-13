using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.Models.DTOs.Complaint
{
    public class ReplyToComplaint
    {
        [Required(ErrorMessage = "Reply message is required")]
        [StringLength(200)]
        public string MessageString { get; set; } = string.Empty;
    }
}
