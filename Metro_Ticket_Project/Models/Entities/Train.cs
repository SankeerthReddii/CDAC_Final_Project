using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("train")]
    public class Train : BaseEntity
    {
        [Required]
        public int Capacity { get; set; }

        // Navigation property
        public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}