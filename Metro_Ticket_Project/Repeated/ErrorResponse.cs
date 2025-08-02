namespace Metro_Ticket_Project.DTO
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Success { get; set; } = false;

        public ErrorResponse(string message, DateTime timestamp)
        {
            Message = message;
            Timestamp = timestamp;
        }
    }
}