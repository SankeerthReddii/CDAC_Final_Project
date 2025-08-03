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

        [StringLength(20)]
        [Column("card_status")]
        public string CardStatus { get; set; } = "Pending";

        [Column("user_id")]
        public int UserId { get; set; }

        // Navigation property
        public virtual User User { get; set; } = null!;
        public int Pin { get; set; }
    }
}