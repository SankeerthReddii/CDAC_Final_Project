////using Metro_Ticket_Booking.DTOs;
////using Metro_Ticket_Booking.Models;
////using Metro_Ticket_Booking.Services;
////using Microsoft.EntityFrameworkCore;
////using System.Security.Cryptography;
////using System.Text;
////using System.Threading.Tasks;

////namespace Metro_Ticket_Booking.Services
////{
////    public class AuthService : IAuthService
////    {
////        private readonly MetroTicketContext _context;

////        public AuthService(MetroTicketContext context)
////        {
////            _context = context;
////        }

////        // ----- USER REGISTRATION -----
////        public async Task<bool> RegisterUserAsync(RegisterUserDto dto)
////        {
////            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
////                return false;

////            var passwordHash = HashPassword(dto.Password);

////            var user = new User
////            {
////                Name = dto.Name,
////                Phone = dto.Phone,
////                Dob = dto.DOB,
////                Address = dto.Address,
////                Gender = dto.Gender,
////                Email = dto.Email,
////                PasswordHash = passwordHash,
////                LoyaltyPoints = 0,
////                CreatedAt = DateTime.UtcNow
////            };

////            _context.Users.Add(user);
////            await _context.SaveChangesAsync();
////            return true;
////        }

////        // ----- USER LOGIN -----
////        public async Task<string> LoginUserAsync(LoginUserDto dto)
////        {
////            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
////            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
////                return null;

////            return "User login successful"; // (Later, return JWT token here)
////        }

////        // ----- ADMIN REGISTRATION -----
////        public async Task<bool> RegisterAdminAsync(RegisterAdminDto dto)
////        {
////            if (await _context.Admins.AnyAsync(a => a.Email == dto.Email))
////                return false;

////            var passwordHash = HashPassword(dto.Password);

////            var admin = new Admin
////            {
////                Name = dto.Name,
////                Email = dto.Email,
////                PasswordHash = passwordHash,
////                CreatedAt = DateTime.UtcNow
////            };

////            _context.Admins.Add(admin);
////            await _context.SaveChangesAsync();
////            return true;
////        }

////        // ----- ADMIN LOGIN -----
////        public async Task<string> LoginAdminAsync(LoginAdminDto dto)
////        {
////            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == dto.Email);
////            if (admin == null || !VerifyPassword(dto.Password, admin.PasswordHash))
////                return null;

////            return "Admin login successful"; // (Later, return JWT token here)
////        }

////        // ----- HELPER METHODS -----
////        private byte[] HashPassword(string password)
////        {
////            using (SHA256 sha256 = SHA256.Create())
////            {
////                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
////            }
////        }

////        private bool VerifyPassword(string password, byte[] storedHash)
////        {
////            var hash = HashPassword(password);
////            return hash.SequenceEqual(storedHash);
////        }
////    }
////}


//using Metro_Ticket_Booking.DTOs;
//using Metro_Ticket_Booking.Models;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace Metro_Ticket_Booking.Services
//{
//    public class AuthService : IAuthService
//    {
//        private readonly MetroTicketContext _context;

//        public AuthService(MetroTicketContext context)
//        {
//            _context = context;
//        }

//        // ----- USER REGISTRATION -----
//        public async Task<bool> RegisterUserAsync(RegisterUserDto dto)
//        {
//            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
//                return false;

//            var passwordHash = HashPassword(dto.Password);

//            var user = new User
//            {
//                Name = dto.Name,
//                Phone = dto.Phone,
//                Dob = dto.Dob,
//                Address = dto.Address,
//                Gender = dto.Gender,
//                Email = dto.Email,
//                PasswordHash = passwordHash,
//                LoyaltyPoints = 0,
//                CreatedAt = DateTime.UtcNow
//            };

//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        // ----- USER LOGIN -----
//        public async Task<AuthResponseDto> LoginUserAsync(LoginUserDto dto)
//        {
//            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
//            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
//                return null;

//            return new AuthResponseDto
//            {
//                Id = user.UserId.ToString(),
//                Name = user.Name,
//                Email = user.Email,
//                Role = "user"
//            };
//        }

//        // ----- ADMIN REGISTRATION -----
//        public async Task<bool> RegisterAdminAsync(RegisterAdminDto dto)
//        {
//            if (await _context.Admins.AnyAsync(a => a.Email == dto.Email))
//                return false;

//            var passwordHash = HashPassword(dto.Password);

//            var admin = new Admin
//            {
//                Name = dto.Name,
//                Email = dto.Email,
//                PasswordHash = passwordHash,
//                CreatedAt = DateTime.UtcNow
//            };

//            _context.Admins.Add(admin);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        // ----- ADMIN LOGIN -----
//        public async Task<AuthResponseDto> LoginAdminAsync(LoginAdminDto dto)
//        {
//            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == dto.Email);
//            if (admin == null || !VerifyPassword(dto.Password, admin.PasswordHash))
//                return null;

//            return new AuthResponseDto
//            {
//                Id = admin.AdminId.ToString(),
//                Name = admin.Name,
//                Email = admin.Email,
//                Role = "admin"
//            };
//        }

//        // ----- HELPER METHODS -----
//        private byte[] HashPassword(string password)
//        {
//            using (SHA256 sha256 = SHA256.Create())
//            {
//                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//            }
//        }

//        private bool VerifyPassword(string password, byte[] storedHash)
//        {
//            var hash = HashPassword(password);
//            return hash.SequenceEqual(storedHash);
//        }
//    }
//}






// AuthService.cs
using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Booking.DTOs;
using Metro_Ticket_Booking.Models;
using System.Security.Cryptography;
using System.Text;

namespace Metro_Ticket_Booking.Services
{
    public class AuthService : IAuthService
    {
        private readonly MetroTicketContext _context;

        public AuthService(MetroTicketContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDto dto)
        {
            // Check if user already exists
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return false;

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
            return true;
        }

        public async Task<AuthResponseDto> LoginUserAsync(LoginUserDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return null;

            return new AuthResponseDto
            {
                Id = user.UserId.ToString(),
                Name = user.Name,
                Email = user.Email,
                Role = "user"
            };
        }

        public async Task<bool> RegisterAdminAsync(RegisterAdminDto dto)
        {
            if (await _context.Admins.AnyAsync(a => a.Email == dto.Email))
                return false;

            var admin = new Admin
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AuthResponseDto> LoginAdminAsync(LoginAdminDto dto)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == dto.Email);

            if (admin == null || !VerifyPassword(dto.Password, admin.PasswordHash))
                return null;

            return new AuthResponseDto
            {
                Id = admin.AdminId.ToString(),
                Name = admin.Name,
                Email = admin.Email,
                Role = "admin"
            };
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
    }
}