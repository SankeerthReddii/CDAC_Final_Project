using System;
using System.Collections.Generic;

namespace Metro_Ticket_Booking.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly? Dob { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!; // Stored as Base64 string

    public int? LoyaltyPoints { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public virtual ICollection<MetroCard> MetroCards { get; set; } = new List<MetroCard>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
