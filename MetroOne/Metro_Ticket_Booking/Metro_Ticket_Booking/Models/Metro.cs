using System;
using System.Collections.Generic;

namespace Metro_Ticket_Booking.Models;

public partial class Metro
{
    public int MetroId { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
