using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("histories")]
    public class History : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Column("transaction_type")]
        public string TransactionType { get; set; } = string.Empty;

        [Column("amount", TypeName = "decimal(10,2)")]
        public decimal? Amount { get; set; }

        [StringLength(500)]
        [Column("description")]
        public string? Description { get; set; }

        [StringLength(20)]
        [Column("status")]
        public string? Status { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("metro_card_id")]
        public int? MetroCardId { get; set; }

        [Column("transaction_date")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100)]
        [Column("payment_id", TypeName = "varchar(100)")]
        public string PaymentId { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Source { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Destination { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual MetroCard? MetroCard { get; set; }
    }
}