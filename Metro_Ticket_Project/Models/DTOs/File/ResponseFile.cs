namespace Metro_Ticket_Project.Models.DTOs.File
{
    public class ResponseFile
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public long Size { get; set; }
    }
}