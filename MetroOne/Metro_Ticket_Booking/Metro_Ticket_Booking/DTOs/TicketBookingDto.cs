using System;
using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Booking.DTOs;
public class TicketBookingDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int RouteId { get; set; }

    [Required]
    public int MetroId { get; set; }

    [Required]
    public int FromStationId { get; set; }

    [Required]
    public int ToStationId { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public decimal Price { get; set; }

    [Required]
    public int TicketCount { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime TravelDate { get; set; }

    public string? TravelTime { get; set; }
}
