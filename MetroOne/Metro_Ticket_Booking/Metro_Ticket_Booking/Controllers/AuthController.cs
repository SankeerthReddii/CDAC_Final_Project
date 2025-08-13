using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Metro_Ticket_Booking.DTOs;
using Metro_Ticket_Booking.Services;
using System.Security.Claims;
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

    // ===========================
    // USER REGISTRATION & LOGIN
    // ===========================

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

    // ===========================
    // NEW: GET CURRENT USER
    // ===========================

    [HttpGet("current-user")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        // Get logged-in user ID from JWT claims
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var user = await _authService.GetUserByIdAsync(int.Parse(userId));
        if (user == null)
            return NotFound(new { message = "User not found." });

        return Ok(user);
    }

    // ===========================
    // NEW: UPDATE PROFILE
    // ===========================

    [HttpPut("profile/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateUserDto updates)
    {
        // Ensure user can only update their own profile unless they are admin
        var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var loggedInRole = User.FindFirstValue(ClaimTypes.Role);

        if (loggedInRole != "admin" && loggedInUserId != id.ToString())
            return Forbid();

        var updatedUser = await _authService.UpdateUserProfileAsync(id, updates);
        if (updatedUser == null)
            return NotFound(new { message = "User not found." });

        return Ok(updatedUser);
    }

    // ===========================
    // NEW: GET ALL USERS (ADMIN)
    // ===========================

    [HttpGet("users")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _authService.GetAllUsersAsync();
        return Ok(users);
    }
}
