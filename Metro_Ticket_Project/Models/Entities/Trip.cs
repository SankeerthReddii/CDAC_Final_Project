using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("trip")]
    public class Trip : BaseEntity
    {
        [Required]
        [Column("route_id")]
        public int RouteId { get; set; }

        [Required]
        [ForeignKey("Train")]
        public int TrainId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Start { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string End { get; set; } = string.Empty;

        // Navigation properties
        public virtual Train Train { get; set; } = null!;
        public virtual Route Route { get; set; } = null!;
        public virtual ICollection<TripDetails> TripDetails { get; set; } = new List<TripDetails>();
    }
}