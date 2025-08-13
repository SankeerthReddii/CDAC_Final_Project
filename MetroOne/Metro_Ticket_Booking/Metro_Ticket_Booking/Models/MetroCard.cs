using System;
using System.Collections.Generic;

namespace Metro_Ticket_Booking.Models;

public partial class MetroCard
{
    public int CardId { get; set; }

    public int UserId { get; set; }

    public string NameOnCard { get; set; }
    public string CardType { get; set; }

    public string CardStatus { get; set; } = null!;

    public DateTime? ApplicationDate { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public virtual User User { get; set; } = null!;
    public int Balance { get; internal set; }
}
