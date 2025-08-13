using System.Collections.Generic;
using System.Threading.Tasks;
using Metro_Ticket_Booking.DTOs;
using Metro_Ticket_Booking.Models;

namespace Metro_Ticket_Booking.Services;
public interface IAdminService
{
    Task<AdminDashboardMetricsDto> GetDashboardMetricsAsync();

    // Stations CRUD
    Task<IEnumerable<Station>> GetStationsAsync();
    Task<Station> GetStationByIdAsync(int id);
    Task<Station> CreateStationAsync(Station station);
    Task<Station> UpdateStationAsync(int id, Station station);
    Task<bool> DeleteStationAsync(int id);

    // Metros CRUD
    Task<IEnumerable<Metro>> GetMetrosAsync();
    Task<Metro> GetMetroByIdAsync(int id);
    Task<Metro> CreateMetroAsync(Metro metro);
    Task<Metro> UpdateMetroAsync(int id, Metro metro);
    Task<bool> DeleteMetroAsync(int id);

    // Routes CRUD
    Task<IEnumerable<Metro_Ticket_Booking.Models.Route>> GetRoutesAsync();
    Task<Metro_Ticket_Booking.Models.Route> GetRouteByIdAsync(int id);
    Task<Metro_Ticket_Booking.Models.Route> CreateRouteAsync(Metro_Ticket_Booking.Models.Route route);
    Task<Metro_Ticket_Booking.Models.Route> UpdateRouteAsync(int id, Metro_Ticket_Booking.Models.Route route);
    Task<bool> DeleteRouteAsync(int id);

    // Complaints Management
    Task<IEnumerable<Complaint>> GetPendingComplaintsAsync();
    Task<bool> UpdateComplaintStatusAsync(int complaintId, string newStatus);
    Task<Complaint> UpdateComplaintAsync(Complaint complaint);
    Task<Complaint> GetComplaintByIdAsync(int id);

    // Metrocard Approval
    Task<IEnumerable<MetroCard>> GetPendingMetroCardApplicationsAsync();
    Task<bool> ApproveMetroCardAsync(int cardId);
    Task<bool> RejectMetroCardAsync(int cardId);
}
