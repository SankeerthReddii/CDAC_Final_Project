using Metro_Ticket_Booking.DTOs;

namespace Metro_Ticket_Booking.Services;
public interface IAuthService
{
    Task<bool> RegisterUserAsync(RegisterUserDto dto);
    Task<(AuthResponseDto user, string token)> LoginUserWithJwtAsync(LoginUserDto dto);
    //Task<bool> RegisterAdminAsync(RegisterAdminDto dto);
    Task<(AuthResponseDto admin, string token)> LoginAdminWithJwtAsync(LoginAdminDto dto);

    Task<AuthResponseDto> GetUserByIdAsync(int id);
    Task<IEnumerable<AuthResponseDto>> GetAllUsersAsync();
    Task<AuthResponseDto> UpdateUserProfileAsync(int id, UpdateUserDto dto);
    Task<AuthResponseDto> GetCurrentUserAsync(string userIdFromToken);

}
