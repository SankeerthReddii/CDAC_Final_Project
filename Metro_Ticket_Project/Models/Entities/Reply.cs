using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("replies")]
    public class Reply : BaseEntity
    {
        internal DateTime ReplyDate;

        [Required]
        [Column("complaint_id")]
        public int ComplaintId { get; set; }

        [Required]
        [StringLength(1000)]
        [Column("reply_text")]
        public string ReplyText { get; set; } = string.Empty;

        [StringLength(100)]
        [Column("replied_by")]
        public string? RepliedBy { get; set; }

        [Required]
        [StringLength(200)]
        [Column("msg", TypeName = "varchar(200)")]
        public string Message { get; set; } = string.Empty;

        [Required]
        [Column("admin_id")]
        public int AdminId { get; set; }

        // Navigation properties
        public virtual Complaint Complaint { get; set; } = null!;
        public virtual Admin Admin { get; set; } = null!;
    }
}