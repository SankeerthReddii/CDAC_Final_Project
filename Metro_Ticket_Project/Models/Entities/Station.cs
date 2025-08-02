using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metro_Ticket_Project.Models.Entities
{
    [Table("station")]
    public class Station : BaseEntity
    {
        internal String? Code { get; set; }
        internal String? Location { get; set; }

        [Required]
        [StringLength(30)]
        [Column("s_name", TypeName = "varchar(30)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,8)")]
        public decimal Latitude { get; set; }

        [Required]
        [Column(TypeName = "decimal(11,8)")]
        public decimal Longitude { get; set; }
    }
}