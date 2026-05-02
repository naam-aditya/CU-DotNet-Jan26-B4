using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBookingSystem.AuthService.Dtos;
using TicketBookingSystem.AuthService.Models;

namespace TicketBookingSystem.AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET api/profile  — load profile for logged-in user
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email ?? "");
            if (user == null) return NotFound("User not found.");

            return Ok(new UserProfileDto
            {
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                City = user.City
            });
        }

        // PUT api/profile  — save profile for logged-in user
        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UserProfileDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email ?? "");
            if (user == null) return NotFound("User not found.");

            user.FullName = dto.FullName;
            user.DateOfBirth = dto.DateOfBirth;
            user.Gender = dto.Gender;
            user.PhoneNumber = dto.PhoneNumber;
            user.City = dto.City;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Profile updated successfully." });
        }
    }
}
