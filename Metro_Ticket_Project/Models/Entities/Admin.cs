using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("admin")]
    public class Admin : BaseEntity
    {
        [Required(ErrorMessage = "Email is required!")]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Column("a_name", TypeName = "varchar(30)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Column("phone_num", TypeName = "varchar(30)")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Column("a_address", TypeName = "varchar(30)")]
        public string Address { get; set; } = string.Empty;

        [Required]
        public int Permission { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Designation { get; set; } = string.Empty;
    }
}