using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("fair")]
    public class Fare : BaseEntity
    {
        [Required]
        [Column("src")]
        public int Source { get; set; }

        [Required]
        [Column("dst")]
        public int Destination { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        [Column("dist")]
        public int Distance { get; set; }
    }
}