//using Metro_Ticket_Booking.DTOs;
//using System.Threading.Tasks;

//namespace Metro_Ticket_Booking.Services
//{
//    public interface IAuthService
//    {
//        Task<bool> RegisterUserAsync(RegisterUserDto dto);
//        Task<AuthResponseDto> LoginUserAsync(LoginUserDto dto);
//        Task<bool> RegisterAdminAsync(RegisterAdminDto dto);
//        Task<AuthResponseDto> LoginAdminAsync(LoginAdminDto dto);
//    }
//}

using Metro_Ticket_Booking.DTOs;

namespace Metro_Ticket_Booking.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterUserDto dto);
        Task<AuthResponseDto> LoginUserAsync(LoginUserDto dto);
        Task<bool> RegisterAdminAsync(RegisterAdminDto dto);
        Task<AuthResponseDto> LoginAdminAsync(LoginAdminDto dto);
    }
}