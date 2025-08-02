using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("history")]
    public class History : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Column("transaction_type", TypeName = "varchar(50)")]
        public string TransactionType { get; set; } = string.Empty;

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
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Status { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}