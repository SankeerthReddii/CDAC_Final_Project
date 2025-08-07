using System;
using System.Collections.Generic;

namespace Metro_Ticket_Booking.Models;

public partial class Route
{
    public int RouteId { get; set; }

    public string Name { get; set; } = null!;

    public int StartStationId { get; set; }

    public int EndStationId { get; set; }

    public virtual Station EndStation { get; set; } = null!;

    public virtual Station StartStation { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
