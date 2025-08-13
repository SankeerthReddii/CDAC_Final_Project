using System.ComponentModel.DataAnnotations;

namespace Metro_Ticket_Project.Models.DTOs.Common
{
    public abstract class BaseResponse
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }

    public class ResponseMessage
    {
        private string v;

        public ResponseMessage(string v)
        {
            this.v = v;
        }

        [Required]
        public string Message { get; set; } = string.Empty;
    }

    public class ErrorResponse : BaseResponse
    {
        internal DateTime Timestamp;

        [Required]
        public string Message { get; set; } = string.Empty;
    }
}