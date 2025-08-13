using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.Models.DTOs.Schedule
{
    public class ScheduleRequest
    {
        [Required(ErrorMessage = "Source station ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid source station ID")]
        public int SourceId { get; set; }
    }

    public class ScheduleResponse
    {
        public int TrainNo { get; set; }
        public int TripNo { get; set; }
        public string Towards { get; set; } = string.Empty;
        public string ArrivalTime { get; set; } = string.Empty;
        public string DepartureTime { get; set; } = string.Empty;
    }
}