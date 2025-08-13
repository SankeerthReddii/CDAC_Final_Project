namespace Metro_Ticket_Booking.DTOs.Payments
{
    public class CreateOrderRequestDto
    {
        public decimal Amount { get; set; }            // e.g. 500.00
        public string Currency { get; set; } = "INR";  // Default to INR
        public int TicketId { get; set; }              // Link payment to a ticket

    }
}
