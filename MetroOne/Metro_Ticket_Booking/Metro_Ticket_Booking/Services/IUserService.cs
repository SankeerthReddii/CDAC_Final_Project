using Metro_Ticket_Booking.DTOs;
using Metro_Ticket_Booking.Models;

using Metro_Ticket_Booking.DTOs;
using Metro_Ticket_Booking.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Metro_Ticket_Booking.Services;
public interface IUserService
{
    // Get available stations and routes for booking
    Task<IEnumerable<Station>> GetAvailableStationsAsync();
    Task<IEnumerable<Models.Route>> GetAvailableRoutesAsync();

    // Ticket booking
    Task<Ticket> BookTicketAsync(TicketBookingDto bookingDto);

    Task<MetroCard> GetMetroCardByUserIdAsync(int userId);

    // Apply metrocard to booking
    Task<MetroCard> ApplyMetroCardToBookingAsync(int userId, string nameOnCard, string cardType);

    // Recharge metrocard balance
    Task<bool> RechargeMetroCardAsync(int metroCardId, int amount);

    // Raise complaint
    Task<Complaint> RaiseComplaintAsync(ComplaintCreateDto complaintDto);

    // View complaints of the user
    Task<IEnumerable<Complaint>> GetComplaintsByUserAsync(string userId);

    // View past bookings of the user
    Task<IEnumerable<BookingHistoryDto>> GetBookingsByUserAsync(string userId);
    Task<int> GetTotalBookingsByUserAsync(int userId);
}



//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Metro_Ticket_Booking.DTOs;
//using Metro_Ticket_Booking.Models;

//namespace Metro_Ticket_Booking.Services;
//public interface IUserService
//{
//    // Get available stations and routes for booking
//    Task<IEnumerable<Station>> GetAvailableStationsAsync();
//    Task<IEnumerable<Models.Route>> GetAvailableRoutesAsync();

//    // Ticket booking
//    Task<Ticket> BookTicketAsync(TicketBookingDto bookingDto);

//    // Apply metrocard to booking
//    Task<bool> ApplyMetroCardToBookingAsync(int bookingId, int metroCardId);

//    // Recharge metrocard balance
//    Task<bool> RechargeMetroCardAsync(int metroCardId, int amount);

//    // Raise complaint
//    Task<Complaint> RaiseComplaintAsync(ComplaintCreateDto complaintDto);

//    // View complaints of the user
//    Task<IEnumerable<Complaint>> GetComplaintsByUserAsync(string userId);

//    // View past bookings of the user
//    Task<IEnumerable<Ticket>> GetBookingsByUserAsync(string userId);
//}