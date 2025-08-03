using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("complaints")]
    public class Complaint : BaseEntity
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        [Column("subject")]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [StringLength(20)]
        [Column("status")]
        public string Status { get; set; } = "Pending";

        [StringLength(10)]
        [Column("priority")]
        public string Priority { get; set; } = "Medium";

        [Required]
        [StringLength(20)]
        [Column("u_name", TypeName = "varchar(20)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("u_address", TypeName = "varchar(50)")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Column("phone_num", TypeName = "varchar(20)")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        [Column("msg", TypeName = "varchar(200)")]
        public string Message { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Email { get; set; }

        [Column("date_time")]
        public DateTime DateTime { get; set; } = DateTime.Now;

        [StringLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string? Response { get; set; }

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();
    }
}