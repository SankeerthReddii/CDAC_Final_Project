namespace Metro_Ticket_Booking.DTOs.Payments
{
    public class VerifySignatureRequestDto
    {
        public string OrderId { get; set; }            // Razorpay order_id
        public string PaymentId { get; set; }          // Razorpay payment_id
        public string Signature { get; set; }          // Razorpay signature

    }
}
