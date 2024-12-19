using BloodDonationAPI.Data;
using BloodDonationAPI.DTOs.Admin;
using BloodDonationAPI.Models;
using BloodDonationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")] // Restrict access to admin role
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public AdminController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: api/admin/users
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                BloodType = u.BloodType,
                DateOfBirth = u.DateOfBirth,
                Gender = u.Gender,
                PhoneNumber = u.PhoneNumber,
                Address = u.Address,
                Role = u.Role
            });

            return Ok(userDtos);
        }

        // GET: api/admin/users/{id}
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                BloodType = user.BloodType,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Role = user.Role
            };

            return Ok(userDto);
        }

        // PUT: api/admin/users/{id}
        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            // Update fields if provided
            if (!string.IsNullOrWhiteSpace(updateDto.Username))
                user.Username = updateDto.Username;

            if (!string.IsNullOrWhiteSpace(updateDto.Email))
            {
                // Check if email is already in use by another user
                if (await _context.Users.AnyAsync(u => u.Email == updateDto.Email && u.Id != id))
                {
                    return BadRequest("The new email is already in use.");
                }
                user.Email = updateDto.Email;
            }

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

            if (!string.IsNullOrWhiteSpace(updateDto.Role))
            {
                if (updateDto.Role != "admin" && updateDto.Role != "user")
                {
                    return BadRequest("Invalid role specified.");
                }
                user.Role = updateDto.Role;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.NewPassword))
            {
                // Hash the new password using BCrypt
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(updateDto.NewPassword);
                user.PasswordHash = hashedPassword;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Optionally, send a notification email about profile update
            string subject = "Your Profile Has Been Updated";
            string body = $@"
                <h1>Hello, {user.Username}!</h1>
                <p>Your profile has been successfully updated by an administrator.</p>
                <p>If you did not expect this change, please contact support immediately.</p>
                <p>Best Regards,<br/>Blood Donation Team</p>
            ";

            try
            {
                await _emailService.SendEmailAsync(user.Email, subject, body);
            }
            catch (Exception)
            {
                // Log the exception (implement logging as needed)
                // For now, we'll ignore email sending failures
            }

            return Ok("User updated successfully.");
        }

        // DELETE: api/admin/users/{id}
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            // Optionally, send a confirmation email about profile deletion
            string subject = "Your Profile Has Been Deleted";
            string body = $@"
                <h1>Goodbye, {user.Username}!</h1>
                <p>Your profile has been successfully deleted from our Blood Donation App by an administrator.</p>
                <p>If you did not expect this action, please contact support immediately.</p>
                <p>Best Regards,<br/>Blood Donation Team</p>
            ";

            try
            {
                await _emailService.SendEmailAsync(user.Email, subject, body);
            }
            catch (Exception)
            {
                // Log the exception (implement logging as needed)
                // For now, we'll ignore email sending failures
            }

            return Ok("User deleted successfully.");
        }
    }
}
