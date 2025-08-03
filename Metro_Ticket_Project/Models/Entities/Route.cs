using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("routes")]
    public class Route : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Column("route_name")]
        public string RouteName { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        [Column("route_code")]
        public string RouteCode { get; set; } = string.Empty;

        [Column("total_distance", TypeName = "decimal(8,2)")]
        public decimal? TotalDistance { get; set; }

        // Navigation property for route details
        public virtual ICollection<RouteDetails> RouteDetails { get; set; } = new List<RouteDetails>();
    }
}