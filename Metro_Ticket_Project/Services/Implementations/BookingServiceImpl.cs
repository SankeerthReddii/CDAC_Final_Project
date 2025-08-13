//using Metro_Ticket_Project.Data.Interfaces;
using Metro_Ticket_Project.Models;
using Metro_Ticket_Project.Models.DTOs.Fare;
using Metro_Ticket_Project.Models.DTOs.Ticket;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Implementations
{
    public class BookingServiceImpl : IBookingService
    {
        private readonly IFareRepository _fairRepo;
        private readonly IStationRepository _stationRepo;
        private readonly IPaymentService _payService;

        public BookingServiceImpl(IFareRepository fairRepo, IStationRepository stationRepo, IPaymentService payService)
        {
            _fairRepo = fairRepo;
            _stationRepo = stationRepo;
            _payService = payService;
        }

        public async Task<TicketResponse> BookTicketAsync(TicketRequest request)
        {
            string journey = "";
            int amount = 0;

            int fair = (int)await _fairRepo.GetFareAmountAsync(request.SourceId, request.DestinationId);

            if (request.IsJourneyType)
            {
                journey = "RETURN";
                amount = fair * request.Quantity * 2;
            }
            else
            {
                journey = "ONE-WAY";
                amount = fair * request.Quantity;
            }

            return new TicketResponse(request.SourceId, request.DestinationId, request.Quantity, journey, amount, DateTime.Now);
        }

        public async Task<List<FareResponse>> GetFairFromStationAsync(int id)
        {
            var response = new List<FareResponse>();
            var fairList = await _fairRepo.GetFareFromStationAsync(id);

            foreach (var fair in fairList)
            {
                response.Add(new FareResponse());
            }

            return response;
        }
    }
}