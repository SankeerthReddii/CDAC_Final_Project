using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("reply")]
    public class Reply : BaseEntity
    {
        [Required]
        [Column("complaint_id")]
        public int ComplaintId { get; set; }

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