namespace Metro_Ticket_Booking.DTOs
{
    public class CreateOrUpdateRouteDto
    {
        public string Name { get; set; }
        public int StartStationId { get; set; }   // int, not string
        public int EndStationId { get; set; }     // int, not string
    }

}
