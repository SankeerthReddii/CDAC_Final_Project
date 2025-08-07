// DTOs.cs - All Data Transfer Objects
namespace Metro_Ticket_Booking.DTOs
{
    // Auth DTOs
    public class RegisterUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateOnly Dob { get; set; }
    }

    public class LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterAdminDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginAdminDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public String Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; } // Optional: for JWT tokens
    }

     //Complaint DTOs
    public class ComplaintCreateDto
    {
        public int UserId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
    }

    public class ComplaintResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Subject { get; set; }
        public string Reply { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string AdminResponse { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime? RepliedAt { get; set; }
    }

    public class ComplaintUpdateDto
    {
        public string Status { get; set; }
        public string AdminResponse { get; set; }
    }

    // Ticket DTOs
    public class TicketCreateDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int FromStationId { get; set; }
        public string FromStation { get; set; }
        public int ToStationId { get; set; }
        public string ToStation { get; set; }
        public int RouteId { get; set; }
        public int MetroId { get; set; }
        public decimal Price { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class TicketResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string FromStation { get; set; }
        public string ToStation { get; set; }
        public decimal Price { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime BookingDate { get; set; }
    }

    // MetroCard DTOs
    public class MetroCardCreateDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string CardType { get; set; }
    }

    public class MetroCardResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string CardType { get; set; }
        public string Status { get; set; }
        public decimal Balance { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }

    // Payment DTOs
    public class PaymentCreateDto
    {
        public int TicketId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public string RazorpayOrderId { get; set; }
        public string RazorpayPaymentId { get; set; }
        public string RazorpaySignature { get; set; }
    }

    public class PaymentResponseDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string RazorpayOrderId { get; set; }
        public string RazorpayPaymentId { get; set; }
    }
}