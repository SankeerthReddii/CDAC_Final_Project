using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("trip_details")]
    public class TripDetails : BaseEntity
    {
        [Required]
        [Column("route_id")]
        public int RouteId { get; set; }

        [Required]
        [Column("trip_id")]
        public int TripId { get; set; }

        [Required]
        [Column("station_id")]
        public int StationId { get; set; }

        [Required]
        [Column("station_order")]
        public int StationOrder { get; set; }

        [Column("arrival_time")]
        public DateTime? ArrivalTime { get; set; }

        [Column("departure_time")]
        public DateTime? DepartureTime { get; set; }

        // Keep the original TripNo property for backward compatibility if needed
        [NotMapped]
        public int TripNo
        {
            get => TripId;
            set => TripId = value;
        }

        // Navigation properties
        public virtual Trip Trip { get; set; } = null!;
        public virtual Station Station { get; set; } = null!;
    }
}