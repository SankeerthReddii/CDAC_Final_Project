using Metro_Ticket_Project.Models.Entities;           // For History entity
using Metro_Ticket_Project.Models.DTOs.Transaction;   // For TransactionRequest
using Metro_Ticket_Project.Models.DTOs.User;          // For UserEmailRequest
using Metro_Ticket_Project.Models.DTOs.Notification;  // For NotificationEmail
using Metro_Ticket_Project.Services.Interfaces;       // For repository interfaces
using Metro_Ticket_Project.Services.Utilities;        // For SendMail
using System.Text.Json;

namespace Metro_Ticket_Project.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IHistoryRepository _historyRepo;
        private readonly IStationRepository _stationRepo;
        private readonly SendMail _mail;

        public PaymentService(IHistoryRepository historyRepo, IStationRepository stationRepo, SendMail mail)
        {
            _historyRepo = historyRepo;
            _stationRepo = stationRepo;
            _mail = mail;
        }

        public async Task<object> PaymentAsync(Dictionary<string, object> data)
        {
            try
            {
                // Note: You'll need to add Razorpay .NET SDK or implement HTTP client calls
                // This is a placeholder implementation for Razorpay integration
                var orderRequest = new
                {
                    amount = int.Parse(data["amount"].ToString()!) * 100, // Convert to paise
                    currency = "INR",
                    receipt = data["receipt"]?.ToString() ?? "",
                    payment_capture = 1
                };

                // TODO: Implement actual Razorpay API integration
                // Example using HttpClient:
                /*
                using var httpClient = new HttpClient();
                var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{razorpayKeyId}:{razorpayKeySecret}"));
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);
                
                var json = JsonSerializer.Serialize(orderRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://api.razorpay.com/v1/orders", content);
                */

                Console.WriteLine($"Payment Order Created: {JsonSerializer.Serialize(orderRequest)}");

                return orderRequest; // Return the order request for now
            }
            catch (Exception e)
            {
                Console.WriteLine($"Payment error: {e.Message}");
                throw new Exception($"Payment processing failed: {e.Message}");
            }
        }

        public async Task<string> BookingHistoryAsync(TransactionRequest obj)
        {
            var transaction = new History
            {
                TransactionType = obj.TransactionType,
                PaymentId = obj.PaymentId,
                Amount = obj.Amount,
                Status = obj.Status,
                Email = obj.Email,
                TimeStamp = DateTime.Now
            };

            if (obj.TransactionType.Equals("Ticket Booking", StringComparison.OrdinalIgnoreCase))
            {
                var sourceStation = await _stationRepo.GetStationByIdAsync(obj.Source);
                var destinationStation = await _stationRepo.GetStationByIdAsync(obj.Destination);

                transaction.Source = sourceStation?.Name ?? "Unknown";
                transaction.Destination = destinationStation?.Name ?? "Unknown";

                await _historyRepo.AddAsync(transaction);

                // Send ticket booking confirmation email
                await _mail.SendMailAsync(new NotificationEmail
                {
                    Subject = "Pune Metro Ticket Booking Confirmation",
                    Recipient = obj.Email,
                    Body = $"Hi,\n\nYour Metro ticket has been booked successfully!\n\n" +
                           $"Booking Details:\n" +
                           $"Transaction ID: {obj.PaymentId}\n" +
                           $"Source Station: {transaction.Source}\n" +
                           $"Destination Station: {transaction.Destination}\n" +
                           $"Amount: ₹{obj.Amount}\n" +
                           $"Status: {obj.Status}\n" +
                           $"Booking Time: {transaction.TimeStamp:yyyy-MM-dd HH:mm:ss}\n\n" +
                           $"Thank you for choosing Pune Metro!\n\n" +
                           $"Regards,\nPune Metro Team"
                });
            }
            else if (obj.TransactionType.Equals("Card Recharge", StringComparison.OrdinalIgnoreCase))
            {
                transaction.Source = "";
                transaction.Destination = "";

                await _historyRepo.AddAsync(transaction);

                // Send card recharge confirmation email
                await _mail.SendMailAsync(new NotificationEmail
                {
                    Subject = "Metro Card Recharge Confirmation",
                    Recipient = obj.Email,
                    Body = $"Hi,\n\nYour Metro Card has been recharged successfully!\n\n" +
                           $"Recharge Details:\n" +
                           $"Transaction ID: {obj.PaymentId}\n" +
                           $"Amount: ₹{obj.Amount}\n" +
                           $"Status: {obj.Status}\n" +
                           $"Recharge Time: {transaction.TimeStamp:yyyy-MM-dd HH:mm:ss}\n\n" +
                           $"Thank you for using Pune Metro services!\n\n" +
                           $"Regards,\nPune Metro Team"
                });
            }

            return "Transaction saved successfully!";
        }

        public async Task<List<History>> DisplayHistoryAsync(UserEmailRequest request)
        {
            var historyList = await _historyRepo.FindByEmailAsync(request.Email);
            return historyList.ToList();
        }

        public async Task<int> GetAllTicketsAsync()
        {
            return await _historyRepo.GetCountOfTicketsAsync();
        }

        public async Task<int> GetAllCardRechargeAsync()
        {
            return await _historyRepo.GetCountOfRechargeAsync();
        }
    }
}