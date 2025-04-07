﻿
using EventManagement_Merged_.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EventManagement_Merged_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // 🔐 Get user by ID (Authorized)
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // 🔐 Update user (Authorized)
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto userDto)
        {
            bool success = _userService.UpdateUser(id, userDto.Name, userDto.ContactNumber, userDto.UserType);
            if (!success) return NotFound();
            return Ok(new { message = "User updated successfully." });
        }

        // GET by user type (no auth restriction)
        [HttpGet("type/{userType}")]
        public IActionResult GetUsersByType(string userType)
        {
            var users = _userService.GetUsersByType(userType);
            return Ok(users);
        }

        // Delete user (optional auth if needed)
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            bool deleted = _userService.DeleteUser(id);
            if (!deleted) return NotFound();
            return Ok(new { message = "User deleted successfully." });
        }
        [Authorize]
        [HttpPut("recover/{id}")]
        public IActionResult RecoverUser(int id)
        {
            bool recovered = _userService.RecoverUser(id);
            if (!recovered) return NotFound();
            return Ok(new { message = "User recovered successfully." });
        }


        // Local DTO for update
        public class UpdateUserDto
        {
            public string Name { get; set; }
            public string ContactNumber { get; set; }
            public string UserType { get; set; }
        }


    }
}