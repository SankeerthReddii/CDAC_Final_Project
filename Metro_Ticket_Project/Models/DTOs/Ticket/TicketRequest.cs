using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.Models.DTOs.Ticket
{
    public class TicketRequest
    {
        [Required(ErrorMessage = "Source station is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid source station")]
        public int SourceId { get; set; }

        [Required(ErrorMessage = "Destination station is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid destination station")]
        public int DestinationId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10")]
        public int Quantity { get; set; }

        [Required]
        public bool IsReturnJourney { get; set; } // Better naming than journeyType
        public bool IsJourneyType { get; internal set; }
    }

    public class TicketResponse
    {
        private string journey;
        private int amount;
        private DateTime now;

        public TicketResponse(int sourceId, int destinationId, int quantity, string journey, int amount, DateTime now)
        {
            SourceId = sourceId;
            DestinationId = destinationId;
            Quantity = quantity;
            this.journey = journey;
            this.amount = amount;
            this.now = now;
        }

        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        public int Quantity { get; set; }
        public string JourneyType { get; set; } = string.Empty;
        public decimal Fare { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}