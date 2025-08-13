using System;

namespace Metro_Ticket_Project.Exceptions
{
    public class CustomExceptionHandler : Exception
    {
        public CustomExceptionHandler(string message) : base(message)
        {
        }

        public CustomExceptionHandler(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}