using System;
using System.Collections.Generic;

namespace Metro_Ticket_Booking.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int UserId { get; set; }

    public int FromStationId { get; set; }

    public int ToStationId { get; set; }

    public int RouteId { get; set; }

    public int MetroId { get; set; }

    public int TicketCount { get; set; }

    public decimal Price { get; set; }

    public DateTime? BookingDate { get; set; }

    public virtual Station FromStation { get; set; } = null!;

    public virtual Metro Metro { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Route Route { get; set; } = null!;

    public virtual Station ToStation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
    public DateTime TravelDate { get; internal set; }
}
