using Metro_Ticket_Project.Models;
using Metro_Ticket_Project.Models.DTOs.Transaction;
using Metro_Ticket_Project.Models.DTOs.User;
using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<object> PaymentAsync(Dictionary<string, object> data);
        Task<string> BookingHistoryAsync(TransactionRequest obj);
        Task<List<History>> DisplayHistoryAsync(UserEmailRequest request);
        Task<int> GetAllTicketsAsync();
        Task<int> GetAllCardRechargeAsync();
    }
}