using BloodDonationAPI.Data;
using BloodDonationAPI.DTOs;
using BloodDonationAPI.Models;
using BloodDonationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BCrypt.Net; // Add this using directive

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthController(ApplicationDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return BadRequest("Email is already in use.");
            }

            string role = "user"; // Default role

            // Check if AdminCode is provided and valid
            if (!string.IsNullOrWhiteSpace(registerDto.AdminCode))
            {
                var adminCodeFromConfig = _configuration["AdminRegistration:Code"];
                if (registerDto.AdminCode == adminCodeFromConfig)
                {
                    role = "admin";
                }
                else
                {
                    return BadRequest("Invalid Admin Code.");
                }
            }

            // Hash the password using BCrypt
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                BloodType = registerDto.BloodType,
                DateOfBirth = registerDto.DateOfBirth,
                Gender = registerDto.Gender,
                PhoneNumber = registerDto.PhoneNumber,
                Address = registerDto.Address,
                Role = role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Prepare the email content
            string subject = "Welcome to Blood Donation App!";
            string body = $@"
                <h1>Hello, {user.Username}!</h1>
                <p>Thank you for registering with our Blood Donation App.</p>
                <p>Your registration details:</p>
                <ul>
                    <li>Email: {user.Email}</li>
                    <li>Blood Type: {user.BloodType}</li>
                    <li>Phone Number: {user.PhoneNumber}</li>
                    <li>Role: {user.Role}</li>
                </ul>
                <p>If you have any questions, feel free to contact us.</p>
                <p>Best Regards,<br/>Blood Donation Team</p>
            ";

            // Send the email
            try
            {
                await _emailService.SendEmailAsync(user.Email, subject, body);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                // Optionally, you can choose to rollback user creation if email fails
                return StatusCode(500, "An error occurred while sending the email.");
            }

            return Ok("User registered successfully. A welcome email has been sent.");
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Verify the password using BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]); // Use configuration key

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("BloodType", user.BloodType),
                    new Claim(ClaimTypes.Role, user.Role) // Add Role claim
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:DurationInMinutes"] ?? "30")),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return Ok(new { Token = jwtToken });
        }
    }
}
