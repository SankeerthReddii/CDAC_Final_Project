namespace Metro_Ticket_Booking.DTOs.Payments
{
    public class ProcessPaymentRequestDto
    {
        public int TicketId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public string RazorpayOrderId { get; set; }
        public string RazorpayPaymentId { get; set; }
        public string RazorpaySignature { get; set; }

    }
}
