using System;
using System.Collections.Generic;

namespace Metro_Ticket_Booking.Models;

public partial class Station
{
    public int StationId { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public virtual ICollection<Route> RouteEndStations { get; set; } = new List<Route>();

    public virtual ICollection<Route> RouteStartStations { get; set; } = new List<Route>();

    public virtual ICollection<Ticket> TicketFromStations { get; set; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketToStations { get; set; } = new List<Ticket>();
}
