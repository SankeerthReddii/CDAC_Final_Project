using System;
using System.Collections.Generic;

namespace Metro_Ticket_Booking.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int TicketId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public DateTime? PaymentDate { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public string? RazorpayOrderId { get; set; }

    public string? RazorpayPaymentId { get; set; }

    public string? RazorpaySignature { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;
}
