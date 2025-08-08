public class BookingHistoryDto
{
    public int Id { get; set; }
    public string FromStation { get; set; } = null!;
    public string ToStation { get; set; } = null!;
    public DateTime? BookingDate { get; set; }
    public DateTime? TravelDate { get; set; }
    public string? TravelTime { get; set; }
    public int NumberOfTickets { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Booked";  // or derive dynamically if you have status field
}
