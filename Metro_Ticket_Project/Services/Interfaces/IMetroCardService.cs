using Metro_Ticket_Project.Models;
using Metro_Ticket_Project.Models.DTOs.Card;
using Metro_Ticket_Project.Models.DTOs.User;
using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Services.Interfaces
{
    public interface IMetroCardService
    {
        Task<MetroCard> RequestMetroCardAsync(MetroCardRequest request);
        Task<string> RechargeCardAsync(RechargeRequest request);
        Task<MetroCard> FetchCardDetailsAsync(UserEmailRequest request);
        Task<List<MetroCard>> FetchCardsAsync();
        Task<string> IssueCardAsync(int id);
        Task<int> GetAllApprovedCardsAsync();
        Task<int> GetAllPendingCardRequestAsync();
        Task<MetroCard> CardAuthenticateAsync(RechargeRequest request);
    }
}