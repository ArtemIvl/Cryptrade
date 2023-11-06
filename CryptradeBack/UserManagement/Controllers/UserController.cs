using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(UserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        // POST api/values
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
        public IActionResult Login([FromBody] UserLoginModel model)
        {
            try
            {
                var _token = _userService.AuthenticateUser(model);
                if (_token == null)
                {
                    return Unauthorized("Invalid username or password.");
                }

                return Ok(new { token = _token}); // return token to the client
            }
            catch (Exception ex)
            {
                return BadRequest($"Login failed: {ex.Message}");
            }
        }

        [HttpGet("check-auth")]
        [Authorize]
        public IActionResult CheckAuthentication()
        {
            return Ok("User is authenticated");
        }

        [HttpGet("profile")]
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

        [HttpPut("update")]
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

        [HttpDelete("delete")]
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

