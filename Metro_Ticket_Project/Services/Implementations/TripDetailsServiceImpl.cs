using Metro_Ticket_Project.Models.Entities;           // For TripDetails, Trip entities
using Metro_Ticket_Project.Models.DTOs.Schedule;      // For ScheduleResponse
using Metro_Ticket_Project.Services.Interfaces;       // For repository interfaces

namespace Metro_Ticket_Project.Services.Implementations
{
    public class TripDetailsServiceImpl : ITripDetailsService
    {
        private readonly ITripDetailsRepository _tripDetailRepo;
        private readonly ITripRepository _tripRepo;

        public TripDetailsServiceImpl(ITripDetailsRepository tripDetailRepo, ITripRepository tripRepo)
        {
            _tripDetailRepo = tripDetailRepo;
            _tripRepo = tripRepo;
        }

        public async Task<List<ScheduleResponse>> GetScheduleAsync(int stationId)
        {
            var response = new List<ScheduleResponse>();

            try
            {
                // Get all trip details for the given station
                var tripDetails = await _tripDetailRepo.GetScheduleByIdAsync(stationId);

                foreach (var tripDetail in tripDetails)
                {
                    // Get trip information including train details
                    var trip = await _tripRepo.GetTrainNoByTripIdAsync(tripDetail.TripNo);
                    var towardsStation = await _tripRepo.GetTowardsStationNameAsync(tripDetail.TripNo);

                    if (trip != null)
                    {
                        var scheduleItem = new ScheduleResponse
                        {
                            TrainNo = trip.Train?.Id ?? 0,
                            TripNo = tripDetail.TripNo,
                            Towards = towardsStation ?? "Unknown",
                            ArrivalTime = tripDetail.ArrivalTime.ToString(),
                            DepartureTime = tripDetail.DepartureTime.ToString()
                        };

                        response.Add(scheduleItem);
                    }
                }

                // Sort by arrival time for better user experience
                response = response.OrderBy(s => s.ArrivalTime).ToList();

                Console.WriteLine($"Retrieved {response.Count} schedule items for station {stationId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving schedule for station {stationId}: {ex.Message}");
                throw new Exception($"Failed to retrieve schedule: {ex.Message}");
            }

            return response;
        }

        // Additional helper method to get schedule for a specific time range
        public async Task<List<ScheduleResponse>> GetScheduleByTimeRangeAsync(int stationId, TimeSpan startTime, TimeSpan endTime)
        {
            var allSchedules = await GetScheduleAsync(stationId);

            return allSchedules.Where(s =>
            {
                if (TimeSpan.TryParse(s.ArrivalTime, out var arrivalTime))
                {
                    return arrivalTime >= startTime && arrivalTime <= endTime;
                }
                return false;
            }).ToList();
        }

        // Additional helper method to get next few trains
        public async Task<List<ScheduleResponse>> GetNextTrainsAsync(int stationId, int count = 5)
        {
            var allSchedules = await GetScheduleAsync(stationId);
            var currentTime = DateTime.Now.TimeOfDay;

            return allSchedules.Where(s =>
            {
                if (TimeSpan.TryParse(s.DepartureTime, out var departureTime))
                {
                    return departureTime > currentTime;
                }
                return false;
            })
            .Take(count)
            .ToList();
        }
    }
}