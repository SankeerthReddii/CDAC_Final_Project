using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("booking_history")]
    public class BookingHistory : BaseEntity
    {
        [Required]
        [Column("booking_id")]
        public long BookingId { get; set; }

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
        public decimal Fare { get; set; }

        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; } = string.Empty;
    }
}