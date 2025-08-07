//using Microsoft.AspNetCore.Mvc;
////using Metro_Ticket_Booking.DTOs;
////using Metro_Ticket_Booking.Services;
////using System.Threading.Tasks;

////namespace Metro_Ticket_Booking.Controllers
////{
////    [ApiController]
////    [Route("api/[controller]")]
////    public class AuthController : ControllerBase
////    {
////        private readonly IAuthService _authService;

////        public AuthController(IAuthService authService)
////        {
////            _authService = authService;
////        }

////        // ----- USER REGISTER -----
////        [HttpPost("register/user")]
////        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
////        {
////            var result = await _authService.RegisterUserAsync(dto);
////            if (!result)
////                return BadRequest("Email already exists.");
////            return Ok("User registered successfully.");
////        }

////        // ----- USER LOGIN -----
////        [HttpPost("login/user")]
////        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto dto)
////        {
////            var result = await _authService.LoginUserAsync(dto);
////            if (result == null)
////                return Unauthorized("Invalid credentials.");
////            return Ok(result);
////        }

////        // ----- ADMIN REGISTER -----
////        [HttpPost("register/admin")]
////        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDto dto)
////        {
////            var result = await _authService.RegisterAdminAsync(dto);
////            if (!result)
////                return BadRequest("Email already exists.");
////            return Ok("Admin registered successfully.");
////        }

////        // ----- ADMIN LOGIN -----
////        [HttpPost("login/admin")]
////        public async Task<IActionResult> LoginAdmin([FromBody] LoginAdminDto dto)
////        {
////            var result = await _authService.LoginAdminAsync(dto);
////            if (result == null)
////                return Unauthorized("Invalid credentials.");
////            return Ok(result);
////        }
////    }
////}




//using Microsoft.AspNetCore.Mvc;
//using Metro_Ticket_Booking.DTOs;
//using Metro_Ticket_Booking.Services;
//using System.Threading.Tasks;

//namespace Metro_Ticket_Booking.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly IAuthService _authService;

//        public AuthController(IAuthService authService)
//        {
//            _authService = authService;
//        }

//        // ----- USER REGISTER -----
//        [HttpPost("register/user")]
//        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
//        {
//            var result = await _authService.RegisterUserAsync(dto);
//            if (!result)
//                return BadRequest("Email already exists.");
//            return Ok("User registered successfully.");
//        }

//        // ----- USER LOGIN -----
//        [HttpPost("login/user")]
//        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto dto)
//        {
//            var result = await _authService.LoginUserAsync(dto);
//            if (result == null)
//                return Unauthorized("Invalid credentials.");

//            // Returns AuthResponseDto: { id, name, email, role }
//            return Ok(result);
//        }

//        // ----- ADMIN REGISTER -----
//        [HttpPost("register/admin")]
//        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDto dto)
//        {
//            var result = await _authService.RegisterAdminAsync(dto);
//            if (!result)
//                return BadRequest("Email already exists.");
//            return Ok("Admin registered successfully.");
//        }

//        // ----- ADMIN LOGIN -----
//        [HttpPost("login/admin")]
//        public async Task<IActionResult> LoginAdmin([FromBody] LoginAdminDto dto)
//        {
//            var result = await _authService.LoginAdminAsync(dto);
//            if (result == null)
//                return Unauthorized("Invalid credentials.");

//            // Returns AuthResponseDto: { id, name, email, role }
//            return Ok(result);
//        }
//    }
//}



// Updated AuthController.cs - Replace your existing AuthController
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Booking.Models;
using Metro_Ticket_Booking.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace Metro_Ticket_Booking.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly MetroTicketContext _context;

        public AuthController(MetroTicketContext context)
        {
            _context = context;
        }

        // POST: api/auth/register - For frontend compatibility
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                // Check if user already exists
                if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                {
                    return BadRequest(new { message = "User with this email already exists" });
                }

                // Create new user
                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Address = dto.Address,
                    Gender = dto.Gender,
                    Dob = dto.Dob,
                    PasswordHash = HashPassword(dto.Password),
                    CreatedAt = DateTime.UtcNow,
                    LoyaltyPoints = 0
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Return user data without password
                var result = new
                {
                    user = new
                    {
                        id = user.UserId,
                        name = user.Name,
                        email = user.Email,
                        phone = user.Phone,
                        address = user.Address,
                        gender = user.Gender,
                        dob = user.Dob,
                        loyaltyPoints = user.LoyaltyPoints,
                        createdAt = user.CreatedAt
                    },
                    token = GenerateToken(user.UserId.ToString())
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/auth/login - For frontend compatibility
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == dto.Email);

                if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                {
                    return BadRequest(new { message = "Invalid email or password" });
                }

                var result = new
                {
                    user = new
                    {
                        id = user.UserId,
                        name = user.Name,
                        email = user.Email,
                        phone = user.Phone,
                        address = user.Address,
                        gender = user.Gender,
                        dob = user.Dob,
                        loyaltyPoints = user.LoyaltyPoints,
                        createdAt = user.CreatedAt
                    },
                    token = GenerateToken(user.UserId.ToString())
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/auth/users - For admin access
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _context.Users
                    .Select(u => new
                    {
                        id = u.UserId,
                        name = u.Name,
                        email = u.Email,
                        phone = u.Phone,
                        address = u.Address,
                        gender = u.Gender,
                        dob = u.Dob,
                        loyaltyPoints = u.LoyaltyPoints,
                        createdAt = u.CreatedAt
                    })
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/auth/profile/{id} - For profile updates
        [HttpPut("profile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateProfileDto dto)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                // Update only provided fields
                if (!string.IsNullOrEmpty(dto.Name)) user.Name = dto.Name;
                if (!string.IsNullOrEmpty(dto.Phone)) user.Phone = dto.Phone;
                if (!string.IsNullOrEmpty(dto.Address)) user.Address = dto.Address;
                if (!string.IsNullOrEmpty(dto.Gender)) user.Gender = dto.Gender;
                //if (dto.Dob.HasValue) user.Dob = dto.Dob;
                user.Dob = dto.Dob;
                await _context.SaveChangesAsync();

                var result = new
                {
                    id = user.UserId,
                    name = user.Name,
                    email = user.Email,
                    phone = user.Phone,
                    address = user.Address,
                    gender = user.Gender,
                    dob = user.Dob,
                    loyaltyPoints = user.LoyaltyPoints,
                    createdAt = user.CreatedAt
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }

        private string GenerateToken(string userId)
        {
            // Simple token for testing - implement JWT in production
            return $"token_{userId}_{DateTime.UtcNow.Ticks}";
        }
    }

    // Additional DTO for profile updates
    public class UpdateProfileDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateOnly Dob { get; set; }
    }
}