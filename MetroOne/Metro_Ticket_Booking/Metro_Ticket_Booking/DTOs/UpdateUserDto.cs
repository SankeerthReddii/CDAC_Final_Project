
namespace Metro_Ticket_Booking.DTOs
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateOnly? Dob { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        // Password update can be separate for security
    }
}
