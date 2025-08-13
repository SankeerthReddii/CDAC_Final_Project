using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.Models.DTOs.Fare
{
    public class FareRequest
    {
        [Required(ErrorMessage = "Source station is required")]
        public string Source { get; set; } = string.Empty;
    }

    public class FareResponse
    {
        public string Destination { get; set; } = string.Empty;
        public decimal Fare { get; set; }
        public int Distance { get; set; }
    }
}