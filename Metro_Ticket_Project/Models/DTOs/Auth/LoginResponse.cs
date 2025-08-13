namespace Metro_Ticket_Project.Models.DTOs.Auth
{
    public class LoginResponse
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty; // For JWT token if needed
        public DateTime LoginTime { get; set; } = DateTime.Now;
        public string Message { get; set; } = "Login successful";

        // Default constructor
        public LoginResponse() { }

        // Constructor with email only
        public LoginResponse(string email)
        {
            Email = email;
            LoginTime = DateTime.Now;
            Message = "Login successful";
        }

        // Constructor with email and token
        public LoginResponse(string email, string token)
        {
            Email = email;
            Token = token;
            LoginTime = DateTime.Now;
            Message = "Login successful";
        }
    }
}