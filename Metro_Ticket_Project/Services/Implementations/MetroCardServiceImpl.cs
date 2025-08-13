using Metro_Ticket_Project.Exceptions;
using Metro_Ticket_Project.Models;
using Metro_Ticket_Project.Models.DTOs.Card;
using Metro_Ticket_Project.Models.DTOs.Notification;
using Metro_Ticket_Project.Models.DTOs.User;
using Metro_Ticket_Project.Models.Entities;
using Metro_Ticket_Project.Services.Interfaces;
using Metro_Ticket_Project.Services.Utilities;

namespace Metro_Ticket_Project.Services.Implementations
{
    public class MetroCardServiceImpl : IMetroCardService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMetroCardRepository _metroRepo;
        private readonly SendMail _mail;

        public MetroCardServiceImpl(IUserRepository userRepo, IMetroCardRepository metroRepo, SendMail mail)
        {
            _userRepo = userRepo;
            _metroRepo = metroRepo;
            _mail = mail;
        }

        public async Task<MetroCard> RequestMetroCardAsync(MetroCardRequest request)
        {
            var user = await _userRepo.FindByEmailAsync(request.User);
            Console.WriteLine(user.Card);

            if (user.Card != null)
                return null;

            var card = new MetroCard
            {
                ICard = null, // Identity card image
                Balance = 0,
                CardNo = "",
                ICardNo = request.ICard,
                Pin = request.Pin,
                User = user
            };

            await _metroRepo.SaveAsync(card);
            user.Card = card;
            await _userRepo.SaveAsync(user);

            return card;
        }

        public async Task<string> RechargeCardAsync(RechargeRequest request)
        {
            var card = await CardAuthenticateAsync(request);
            card.Balance = card.Balance + request.Amount;
            await _metroRepo.SaveAsync(card);

            // Uncomment when email service is ready
             await _mail.SendMailAsync(new NotificationEmail("Metro Card Recharge", card.User.Email,
                 $"Hi,{card.User.Name}\nYour MetroCard recharged successfully with amount {request.Amount} !!!\n" +
                 $"Your updated balance is {card.Balance}"));

            return "Card recharge successfully!!!";
        }

        public async Task<MetroCard> FetchCardDetailsAsync(UserEmailRequest request)
        {
            Console.WriteLine(request.Email);
            var user = await _userRepo.FindByEmailAsync(request.Email);
            int id = user.Id;
            Console.WriteLine(id);
            return await _metroRepo.GetByUserIdAsync(id);
        }

        public async Task<string> IssueCardAsync(int id)
        {
            var random = new Random();
            long number = (long)Math.Floor(random.NextDouble() * 9_000_000_000L) + 1_000_000_000L;
            var card = await _metroRepo.GetByIdAsync(id);
            card.CardStatus = true;
            card.CardNo = number.ToString();
            await _metroRepo.SaveAsync(card);
            return "Card Issued Successfully!!!";
        }

        public async Task<IEnumerable<MetroCard>> FetchCardsAsync()
        {
            return await _metroRepo.GetAllPendingCardsAsync();
        }

        public async Task<int> GetAllApprovedCardsAsync()
        {
            return await _metroRepo.GetCountOfApprovedCardsAsync();
        }

        public async Task<int> GetAllPendingCardRequestAsync()
        {
            return await _metroRepo.GetAllPendingCardRequestAsync();
        }

        public async Task<MetroCard> CardAuthenticateAsync(RechargeRequest request)
        {
            var card = await _metroRepo.FindByCardNoAndPinAsync(request.CardNo, request.Pin);
            if (card == null)
            {
                throw new CustomExceptionHandler("Invalid card details or password...");
            }
            return card;
        }

        Task<List<MetroCard>> IMetroCardService.FetchCardsAsync()
        {
            throw new NotImplementedException();
        }
    }
}