using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("route_details")]
    public class RouteDetails : BaseEntity
    {
        [Required]
        [Column("route_id")]
        public int RouteId { get; set; }

        [Required]
        [Column("station_id")]
        public int StationId { get; set; }

        [Required]
        [Column("station_order")]
        public int StationOrder { get; set; }

        [Column("distance_from_previous", TypeName = "decimal(6,2)")]
        public decimal? DistanceFromPrevious { get; set; }

        // Navigation properties
        public virtual Route Route { get; set; } = null!;
        public virtual Station Station { get; set; } = null!;
    }
}