using System.Security.Claims;
using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtTokenHandler _jwtTokenHandler;

        public UserController(UserService userService, JwtTokenHandler jwtTokenHandler)
        {
            _userService = userService;
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterModel model)
        {
            try
            {
                _userService.RegisterUser(model);
                return Ok("Registration successful");
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse?>> LoginAsync([FromBody] AuthenticationRequest request)
        {
            var userData = await _userService.GetAllUsersAsync();

            var loginuser = userData.Where(u => u.email == request.email);

            if (loginuser == null)
            {
                return BadRequest();
            }

            var response = _jwtTokenHandler.GenerateJwtToken(request, (JwtAuthenticationManager.Entity.User)loginuser);
            if (response == null) return Unauthorized();
            return Ok(response);
        }

        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var userData = await _userService.GetAllUsersAsync();
                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest($"Getting data failed: {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUserData()
        {
            try
            {
                // Extract user's identity from the token
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Fetch user data based on the user ID
                var userData = _userService.GetUserDataById(userId);

                if (userData != null)
                {
                    return Ok(userData);
                }
                else
                {
                    return NotFound("User data not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Getting data failed: {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateUserData([FromBody] UserDataModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                try
                {
                    _userService.UpdateUserData(userId, model.name, model.email);
                    return Ok("User data updated successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to update user data: {ex.Message}");
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                try
                {
                    _userService.DeleteUser(userId);
                    return Ok("User account deleted successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to delete user account: {ex.Message}");
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

