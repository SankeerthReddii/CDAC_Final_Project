using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("stations")]
    public class Station : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        [Column("code")]
        public string Code { get; set; } = string.Empty;

        [StringLength(200)]
        [Column("location")]
        public string? Location { get; set; }
    }
}