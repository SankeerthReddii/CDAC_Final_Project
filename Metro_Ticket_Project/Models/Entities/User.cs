using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [StringLength(15)]
        [Column("phone")]
        public string? Phone { get; set; }

        [StringLength(500)]
        [Column("address")]
        public string? Address { get; set; }

        [Column("dob")]
        public DateTime DateOfBirth { get; set; }

        [Column("gender")]
        public string Gender { get; set; } = string.Empty;

        // navigation property
        public virtual MetroCard? Card { get; set; }
    }
}
