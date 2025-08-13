using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("metro_cards")]
    public class MetroCard : BaseEntity
    {
        [Required]
        [StringLength(16)]
        [Column("card_no")]
        public string CardNo { get; set; } = string.Empty;

        [Column("balance", TypeName = "decimal(10,2)")]
        public decimal Balance { get; set; } = 0;

        [Column("card_status")]
        public bool CardStatus { get; set; } = false;

        // Foreign key property
        [Column("user_id")]
        public int UserId { get; set; }

        // Navigation property (required for EF Core)
        public virtual User User { get; set; } = null!;

        [Column("pin")]
        public int Pin { get; set; }

        [Column("icard")]
        public byte[]? ICard { get; set; }

        [Column("icard_no")]
        public string ICardNo { get; set; } = string.Empty;
    }
}
