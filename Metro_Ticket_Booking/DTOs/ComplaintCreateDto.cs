using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Booking.DTOs
{
    public class ComplaintCreateDto
    {
        [Required]
        public int UserId { get; set; }

        // Optional booking association
        public int BookingId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }
    }



    namespace Metro_Ticket_Booking.DTOs
    {
        public class ComplaintUpdateDto
        {
            public string Status { get; set; }  // Optional field to store reply
        }
    }


}
