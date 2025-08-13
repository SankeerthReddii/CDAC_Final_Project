using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using Metro_Ticket_Project.Models.DTOs.Notification;
using Metro_Ticket_Project.Exceptions;

namespace Metro_Ticket_Project.Services.Utilities
{
    public class SendMail
    {
        private readonly IConfiguration _configuration;
        private readonly string _fromEmail;

        public SendMail(IConfiguration configuration)
        {
            _configuration = configuration;
            _fromEmail = _configuration["EmailSettings:From"] ?? "";
        }

        /// <summary>
        /// Sends email asynchronously with QR code attachment
        /// </summary>
        /// <param name="notificationEmail">Email notification details</param>
        public async Task SendMailAsync(NotificationEmail notificationEmail)
        {
            try
            {
                var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpHost"])
                {
                    Port = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "587"),
                    Credentials = new NetworkCredential(
                        _configuration["EmailSettings:Username"],
                        _configuration["EmailSettings:Password"]),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    Subject = notificationEmail.Subject,
                    Body = $"{notificationEmail.Body}<html><body><img src='cid:identifier1234'></body></html>",
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(notificationEmail.Recipient);

                // Add QR code attachment if file exists
                var qrCodePath = "./MyQRCode.png";
                if (File.Exists(qrCodePath))
                {
                    var attachment = new Attachment(qrCodePath);
                    attachment.ContentId = "identifier1234";
                    mailMessage.Attachments.Add(attachment);
                }

                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception occurred when sending mail: {e.Message}");
                throw new CustomExceptionHandler(
                    $"Exception occurred when sending mail to {notificationEmail.Recipient}");
            }
        }
    }
}