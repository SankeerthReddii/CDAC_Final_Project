using System;
using System.Collections.Generic;

namespace Metro_Ticket_Booking.Models;

public partial class Complaint
{
    public int ComplaintId { get; set; }

    public int UserId { get; set; }

    public string Message { get; set; } = null!;

    public string? Reply { get; set; }

    public string? Status { get; set; }

    public DateTime SubmittedAt { get; set; }

    public DateTime? RepliedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
