using Metro_Ticket_Booking.DTOs;
using Metro_Ticket_Booking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Metro_Ticket_Booking.Services
{
    public class AuthService : IAuthService
    {
        private readonly MetroTicketContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(MetroTicketContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Register user with hashed password as Base64 string
        public async Task<bool> RegisterUserAsync(RegisterUserDto dto)
        {
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

        // Login user and generate JWT
        public async Task<(AuthResponseDto, string)> LoginUserWithJwtAsync(LoginUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return (null, null);

            var userDto = new AuthResponseDto
            {
                Id = user.UserId.ToString(),
                Name = user.Name,
                Email = user.Email,
                Role = "user"
            };

            var token = GenerateJwtToken(userDto);
            return (userDto, token);
        }

        // Login admin and generate JWT
        public async Task<(AuthResponseDto, string)> LoginAdminWithJwtAsync(LoginAdminDto dto)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == dto.Email);

            if (admin == null)
                return (null, null);

            string hashedInputPassword = HashPassword(dto.Password);
            Console.WriteLine(hashedInputPassword);
            if (!string.Equals(admin.PasswordHash, hashedInputPassword))
                return (null, null);

            var adminDto = new AuthResponseDto
            {
                Id = admin.AdminId.ToString(),
                Name = admin.Name,
                Email = admin.Email,
                Role = "admin"
            };

            var token = GenerateJwtToken(adminDto);
            return (adminDto, token);
        }


        // Example hashing function (modify according to your hashing approach)
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            string hex = BitConverter.ToString(hash).Replace("-", "").ToLower();
            byte[] hexBytes = Encoding.UTF8.GetBytes(hex);
            Console.WriteLine(password);
            return Convert.ToBase64String(hexBytes);

        }




        // Verify password by comparing Base64 strings
        private bool VerifyPassword(string password, string storedHashedPassword)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == storedHashedPassword;
        }

        // Generate JWT token
        private string GenerateJwtToken(AuthResponseDto dto)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"]);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, dto.Id),
                new Claim(ClaimTypes.NameIdentifier, dto.Id),
                new Claim(JwtRegisteredClaimNames.Name, dto.Name),
                new Claim(JwtRegisteredClaimNames.Email, dto.Email),
                new Claim(ClaimTypes.Role, dto.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthResponseDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new AuthResponseDto
            {
                Id = user.UserId.ToString(),
                Name = user.Name,
                Email = user.Email,
                Role = "user" // You can adjust if roles are stored differently
            };
        }

        public async Task<AuthResponseDto> UpdateUserProfileAsync(int id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            // Update only provided fields
            if (!string.IsNullOrEmpty(dto.Name)) user.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Phone)) user.Phone = dto.Phone;
            if (dto.Dob.HasValue) user.Dob = dto.Dob.Value;
            if (!string.IsNullOrEmpty(dto.Address)) user.Address = dto.Address;
            if (!string.IsNullOrEmpty(dto.Gender)) user.Gender = dto.Gender;
            if (!string.IsNullOrEmpty(dto.Email)) user.Email = dto.Email;

            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Id = user.UserId.ToString(),
                Name = user.Name,
                Email = user.Email,
                Role = "user"
            };
        }

        public async Task<IEnumerable<AuthResponseDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new AuthResponseDto
                {
                    Id = u.UserId.ToString(),
                    Name = u.Name,
                    Email = u.Email,
                    Role = "user"
                })
                .ToListAsync();
        }

        public async Task<AuthResponseDto> GetCurrentUserAsync(string userIdFromToken)
        {
            if (!int.TryParse(userIdFromToken, out var userId))
                return null;

            return await GetUserByIdAsync(userId);
        }

    }
}
