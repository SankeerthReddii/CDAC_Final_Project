using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("trips")]
    public class Trip : BaseEntity
    {
        [Required]
        [StringLength(20)]
        [Column("trip_code")]
        public string TripCode { get; set; } = string.Empty;

        [Required]
        [Column("route_id")]
        public int RouteId { get; set; }

        [Required]
        [Column("train_id")]
        public int TrainId { get; set; }

        [Required]
        [Column("departure_time")]
        public DateTime DepartureTime { get; set; }

        [Required]
        [Column("arrival_time")]
        public DateTime ArrivalTime { get; set; }

        [StringLength(20)]
        [Column("status")]
        public string Status { get; set; } = "Scheduled";

        [Required]
        [Column("start", TypeName = "varchar(50)")]
        public string Start { get; set; } = string.Empty;

        [Required]
        [Column("end", TypeName = "varchar(50)")]
        public string End { get; set; } = string.Empty;

        // Navigation properties
        public virtual Train Train { get; set; } = null!;
        public virtual Route Route { get; set; } = null!;
        public virtual ICollection<TripDetails> TripDetails { get; set; } = new List<TripDetails>();
    }
}