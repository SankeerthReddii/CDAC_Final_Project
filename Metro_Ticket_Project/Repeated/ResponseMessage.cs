namespace Metro_Ticket_Project.DTO
{
    public class ResponseMessage
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = true;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public ResponseMessage() { }

        public ResponseMessage(string message, bool success = true)
        {
            Message = message;
            Success = success;
        }
    }
}