//using System.ComponentModel.DataAnnotations;

//namespace Metro_Ticket_Project.Models.DTOs.Notification
//{
//    public class NotificationEmail
//    {
//        private string v1;
//        private string email;
//        private string v2;

//        public NotificationEmail(string v1, string email, string v2)
//        {
//            this.v1 = v1;
//            this.email = email;
//            this.v2 = v2;
//        }

//        [Required(ErrorMessage = "Subject is required")]
//        public string Subject { get; set; } = string.Empty;

//        [Required(ErrorMessage = "Recipient email is required")]
//        [EmailAddress(ErrorMessage = "Invalid recipient email format")]
//        public string Recipient { get; set; } = string.Empty;

//        [Required(ErrorMessage = "Email body is required")]
//        public string Body { get; set; } = string.Empty;
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.Models.DTOs.Notification
{
    public class NotificationEmail
    {
        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Recipient email is required")]
        [EmailAddress(ErrorMessage = "Invalid recipient email format")]
        public string Recipient { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email body is required")]
        public string Body { get; set; } = string.Empty;

        // Default parameterless constructor (allows object initializer syntax)
        public NotificationEmail() { }

        // Constructor with parameters (the one your code is expecting)
        public NotificationEmail(string subject, string recipient, string body)
        {
            Subject = subject;
            Recipient = recipient;
            Body = body;
        }
    }
}