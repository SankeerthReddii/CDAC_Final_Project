using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("trains")]
    public class Train : BaseEntity
    {
        [Required]
        [StringLength(20)]
        [Column("train_number")]
        public string TrainNumber { get; set; } = string.Empty;

        [StringLength(100)]
        [Column("train_name")]
        public string? TrainName { get; set; }

        [Required]
        [Column("capacity")]
        public int Capacity { get; set; } = 300;

        [StringLength(20)]
        [Column("status")]
        public string Status { get; set; } = "Active";

        // Navigation property
        public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}