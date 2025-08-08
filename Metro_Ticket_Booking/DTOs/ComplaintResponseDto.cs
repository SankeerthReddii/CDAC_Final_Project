namespace Metro_Ticket_Booking.DTOs
{
    public class ComplaintResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Subject { get; set; }
        public string Reply { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime? RepliedAt { get; set; }
    }
}
