using LoanManagementAPI.DTOs;
using LoanManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginsController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginsController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/Logins/{username}/{password}
        
        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<LoginResponseDto>> Login(string username, string password)
        {
            var result = await _userService.LoginAsync(username, password);
            if (result == null) return Unauthorized(new { message = "Invalid username or password." });

            return Ok(result);
        }

        [HttpPost("register/customer")]
        public async Task<IActionResult> RegisterCustomer(RegisterCustomerDto dto)
        {
            try
            {
                var user = await _userService.RegisterCustomerAsync(dto);
                return Ok(new { user.UserId, user.UserName, user.Status });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost("register/officer")]
        public async Task<IActionResult> RegisterOfficer(RegisterOfficerDto dto)
        {
            try
            {
                var user = await _userService.RegisterOfficerAsync(dto);
                return Ok(new { user.UserId, user.UserName, user.Status });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var success = await _userService.ChangePasswordAsync(dto);
            if (!success) return BadRequest(new { message = "Old password is incorrect." });
            return Ok(new { message = "Password changed successfully." });
        }
    }
}
