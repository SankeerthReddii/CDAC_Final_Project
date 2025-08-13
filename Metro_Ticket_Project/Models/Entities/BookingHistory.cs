using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("booking_histories")]
    public class BookingHistory : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Column("ticket_id")]
        public string TicketId { get; set; } = string.Empty;

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("from_station_id")]
        public int FromStationId { get; set; }

        [Required]
        [Column("to_station_id")]
        public int ToStationId { get; set; }

        [Required]
        [Column("amount", TypeName = "decimal(8,2)")]
        public decimal Amount { get; set; }

        [StringLength(20)]
        [Column("payment_method")]
        public string? PaymentMethod { get; set; }

        [StringLength(20)]
        [Column("payment_status")]
        public string PaymentStatus { get; set; } = "Pending";

        [StringLength(20)]
        [Column("ticket_status")]
        public string TicketStatus { get; set; } = "Booked";

        [Column("booking_date")]
        public DateTime BookingDate { get; set; } = DateTime.Now;

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

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual Station FromStation { get; set; } = null!;
        public virtual Station ToStation { get; set; } = null!;
    }
}