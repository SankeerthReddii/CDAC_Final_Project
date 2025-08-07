//using System;
//using System.ComponentModel.DataAnnotations;

//namespace Metro_Ticket_Booking.DTOs
//{
//    public class RegisterUserDto
//    {
//        [Required]
//        public string Name { get; set; }

//        [Required]
//        [StringLength(10, MinimumLength = 10)]
//        public string Phone { get; set; }

//        [Required]
//        public DateOnly DOB { get; set; }

//        public string Address { get; set; }

//        [Required]
//        [RegularExpression("male|female|other", ErrorMessage = "Gender must be 'male', 'female', or 'other'")]
//        public string Gender { get; set; }

//        [Required]
//        [EmailAddress]
//        public string Email { get; set; }

//        [Required]
//        [MinLength(6)]
//        public string Password { get; set; }
//    }
//}
