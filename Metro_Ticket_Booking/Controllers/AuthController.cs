using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Metro_Ticket_Booking.DTOs;
using Metro_Ticket_Booking.Services;
using System.Threading.Tasks;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        var result = await _authService.RegisterUserAsync(dto);
        if (!result)
            return BadRequest(new { message = "Email already exists." });
        return Ok(new { message = "User registered successfully." });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        var (userResponse, userToken) = await _authService.LoginUserWithJwtAsync(dto);
        if (userResponse != null)
            return Ok(new { user = userResponse, token = userToken, role = userResponse.Role });

        // Try admin login
        var adminDto = new LoginAdminDto { Email = dto.Email, Password = dto.Password };
        var (adminResponse, adminToken) = await _authService.LoginAdminWithJwtAsync(adminDto);
        if (adminResponse != null)
            return Ok(new { user = adminResponse, token = adminToken, role = adminResponse.Role });

        return Unauthorized(new { message = "Invalid email or password." });
    }
}
