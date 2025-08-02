using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("route")]
    public class Route : BaseEntity
    {
        [Required]
        [StringLength(30)]
        [Column("r_name", TypeName = "varchar(30)")]
        public string Name { get; set; } = string.Empty;

        // Navigation property for route details
        public virtual ICollection<RouteDetails> RouteDetails { get; set; } = new List<RouteDetails>();
    }
}