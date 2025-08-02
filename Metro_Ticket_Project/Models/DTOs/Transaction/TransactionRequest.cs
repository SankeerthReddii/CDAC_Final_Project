using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.Models.DTOs.Transaction
{
    public class TransactionRequest
    {
        [Required(ErrorMessage = "Transaction type is required")]
        [StringLength(50)]
        public string TransactionType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Payment ID is required")]
        [StringLength(100)]
        public string PaymentId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Source is required")]
        public int Source { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        public int Destination { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(20)]
        public string Status { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;
    }
}