// DTOs/ReplyToComplaint.cs
using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.DTO
{
    public class ReplyToComplaint
    {
        [Required]
        [StringLength(500)]
        public string Message { get; set; } = string.Empty;
        public string? MsgString { get; internal set; }
    }
}