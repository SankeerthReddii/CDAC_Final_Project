using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("fares")]
    public class Fare : BaseEntity
    {
        [Required]
        [Column("from_station_id")]
        public int FromStationId { get; set; }

        [Required]
        [Column("to_station_id")]
        public int ToStationId { get; set; }

        [Required]
        [Column("amount", TypeName = "decimal(8,2)")]
        public decimal Amount { get; set; }

        [Column("distance", TypeName = "decimal(6,2)")]
        public decimal? Distance { get; set; }

        // Navigation properties
        public virtual Station Source { get; set; } = null!;
        public virtual Station Destination { get; set; } = null!;
    }
}