//using Metro_Ticket_Project.Models.Entities;

namespace Metro_Ticket_Project.Models.DTOs.Admin
{
    public class AdminDataResponse
    {
        public int TotalTickets { get; set; }
        public int TotalRecharge { get; set; }
        public int TotalCards { get; set; }
        public int TotalPendingCards { get; set; }
        public List<Metro_Ticket_Project.Models.Entities.Complaint> TotalComplaints { get; set; }

        // Use correct namespace or class name here if Complaint is ambiguous
        public List<Models.Entities.Complaint> GetAllPendingComplaintsAsync { get; set; }
    }
}
