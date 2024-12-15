// File: BloodDonationAPI/Controllers/ProfileController.cs

using BloodDonationAPI.Data;
using BloodDonationAPI.DTOs;
using BloodDonationAPI.Models;
using BloodDonationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public ProfileController(ApplicationDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        // GET: api/profile
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var user = await _context.Users.FindAsync(userId.Value);
            if (user == null)
                return NotFound("User not found.");

            var profile = new
            {
                user.Id,
                user.Username,
                user.Email,
                user.BloodType,
                user.DateOfBirth,
                user.Gender,
                user.PhoneNumber,
                user.Address
            };

            return Ok(profile);
        }

        // PUT: api/profile
        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var user = await _context.Users.FindAsync(userId.Value);
            if (user == null)
                return NotFound("User not found.");

            // Update fields if provided
            if (!string.IsNullOrWhiteSpace(updateDto.Username))
                user.Username = updateDto.Username;

            if (!string.IsNullOrWhiteSpace(updateDto.BloodType))
                user.BloodType = updateDto.BloodType;

            if (updateDto.DateOfBirth.HasValue)
                user.DateOfBirth = updateDto.DateOfBirth.Value;

            if (!string.IsNullOrWhiteSpace(updateDto.Gender))
                user.Gender = updateDto.Gender;

            if (!string.IsNullOrWhiteSpace(updateDto.PhoneNumber))
                user.PhoneNumber = updateDto.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(updateDto.Address))
                user.Address = updateDto.Address;

            if (!string.IsNullOrWhiteSpace(updateDto.NewEmail))
            {
                if (await _context.Users.AnyAsync(u => u.Email == updateDto.NewEmail && u.Id != userId.Value))
                {
                    return BadRequest("The new email is already in use.");
                }
                user.Email = updateDto.NewEmail;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.NewPassword))
            {
                using var sha256 = SHA256.Create();
                var hashedPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(updateDto.NewPassword)));
                user.PasswordHash = hashedPassword;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Optionally, send a notification email about profile update
            string subject = "Your Profile Has Been Updated";
            string body = $@"
                <h1>Hello, {user.Username}!</h1>
                <p>Your profile has been successfully updated.</p>
                <p>If you did not perform this action, please contact support immediately.</p>
                <p>Best Regards,<br/>Blood Donation Team</p>
            ";

            try
            {
                await _emailService.SendEmailAsync(user.Email, subject, body);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                // For now, we'll ignore email sending failures
            }

            return Ok("Profile updated successfully.");
        }

        // DELETE: api/profile
        [HttpDelete]
        public async Task<IActionResult> DeleteProfile([FromBody] DeleteProfileDto deleteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var user = await _context.Users.FindAsync(userId.Value);
            if (user == null)
                return NotFound("User not found.");

            // Verify password before deletion
            using var sha256 = SHA256.Create();
            var hashedInputPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(deleteDto.Password)));
            if (user.PasswordHash != hashedInputPassword)
            {
                return Unauthorized("Incorrect password.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            // Optionally, send a confirmation email about profile deletion
            string subject = "Your Profile Has Been Deleted";
            string body = $@"
                <h1>Goodbye, {user.Username}!</h1>
                <p>Your profile has been successfully deleted from our Blood Donation App.</p>
                <p>If you did not perform this action, please contact support immediately.</p>
                <p>Best Regards,<br/>Blood Donation Team</p>
            ";

            try
            {
                await _emailService.SendEmailAsync(user.Email, subject, body);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                // For now, we'll ignore email sending failures
            }

            return Ok("Profile deleted successfully.");
        }

        // Helper method to get the current user's ID from JWT claims
        private int? GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
                return userId;
            return null;
        }
    }
}
