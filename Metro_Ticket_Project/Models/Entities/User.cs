using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("user")]
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "Email is required!")]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Column("u_name", TypeName = "varchar(30)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Column("phone_num", TypeName = "varchar(30)")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("u_address", TypeName = "varchar(50)")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Column("dob")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Gender { get; set; } = string.Empty;

        // One-to-One relationship with MetroCard
        public virtual MetroCard? Card { get; set; }
    }
}