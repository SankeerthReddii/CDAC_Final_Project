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

        [StringLength(20)]
        [Column("status", TypeName = "varchar(20)")]
        public string? Status { get; set; }

        [Column("date_time")]
        public DateTime DateTime { get; set; } = DateTime.Now;

        [StringLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string? Response { get; set; }

        // Navigation property
        public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();
    }
}
