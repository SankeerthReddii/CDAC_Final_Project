using System;
using System.Collections.Generic;

namespace Metro_Ticket_Booking.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public String PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
