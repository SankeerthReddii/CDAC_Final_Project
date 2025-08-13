using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.Models.DTOs.Card
{
    public class MetroCardRequest
    {
        [Required(ErrorMessage = "User email is required")]
        public string User { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID card number is required")]
        [StringLength(20)]
        public string ICard { get; set; } = string.Empty;

        [Required(ErrorMessage = "PIN is required")]
        [Range(1000, 9999, ErrorMessage = "PIN must be 4 digits")]
        public int Pin { get; set; }
    }

    public class RechargeRequest
    {
        [Required(ErrorMessage = "Card number is required")]
        [StringLength(30)]
        public string CardNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "PIN is required")]
        [Range(1000, 9999, ErrorMessage = "PIN must be 4 digits")]
        public int Pin { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(1, 10000, ErrorMessage = "Amount must be between 1 and 10,000")]
        public decimal Amount { get; set; }
    }
}