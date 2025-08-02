using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("metro_card")]
    public class MetroCard : BaseEntity
    {
        [StringLength(30)]
        [Column("card_no", TypeName = "varchar(30)")]
        public string? CardNo { get; set; }

        [Required]
        [StringLength(20)]
        [Column("i_card_no", TypeName = "varchar(20)")]
        public string ICardNo { get; set; } = string.Empty;

        [Required]
        public int Pin { get; set; }

        [Column("card_status")]
        public bool CardStatus { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Balance { get; set; }

        [Column("i_card")]
        public byte[]? ICard { get; set; }

        // Foreign Key
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Navigation Property
        public virtual User User { get; set; } = null!;
        //public object CardNumber { get; internal set; }
    }
}