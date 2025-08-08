using System;

namespace Metro_Ticket_Booking.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; }
    // Stored as Base64 string

    public DateTime? CreatedAt { get; set; }
}
