// File: BloodDonationAPI/Controllers/RegistrationsController.cs

using BloodDonationAPI.Data;
using BloodDonationAPI.DTOs;
using BloodDonationAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RegistrationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/registrations
        [HttpPost]
        public async Task<IActionResult> RegisterForEvent([FromBody] CreateRegistrationDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var eventEntity = await _context.Events.FindAsync(createDto.EventId);

            if (eventEntity == null)
                return NotFound("Event not found.");

            // Check if the user is already registered for the event
            bool alreadyRegistered = await _context.EventRegistrations
                .AnyAsync(er => er.EventId == createDto.EventId && er.UserId == userId);

            if (alreadyRegistered)
                return BadRequest("You are already registered for this event.");

            var registration = new EventRegistration
            {
                EventId = createDto.EventId,
                UserId = userId,
                RegisteredAt = DateTime.UtcNow
            };

            _context.EventRegistrations.Add(registration);
            await _context.SaveChangesAsync();

            return Ok("Successfully registered for the event.");
        }

        // DELETE: api/registrations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            var registration = await _context.EventRegistrations.FindAsync(id);
            if (registration == null)
                return NotFound("Registration not found.");

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            // Only the user who registered or an admin can delete the registration
            if (registration.UserId != userId && userRole != "admin")
                return Forbid("You are not authorized to delete this registration.");

            _context.EventRegistrations.Remove(registration);
            await _context.SaveChangesAsync();

            return Ok("Registration deleted successfully.");
        }

        // GET: api/registrations/my
        [HttpGet("my")]
        public async Task<IActionResult> GetMyRegistrations()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var registrations = await _context.EventRegistrations
                .Where(er => er.UserId == userId)
                .Include(er => er.Event)
                .Include(er => er.User)
                .OrderByDescending(er => er.RegisteredAt)
                .ToListAsync();

            var registrationDtos = registrations.Select(er => new RegistrationDto
            {
                Id = er.Id,
                EventId = er.EventId,
                EventTitle = er.Event.Title,
                EventDate = er.Event.EventDate,
                EventLocation = er.Event.Location,
                UserId = er.UserId,
                Username = er.User.Username,
                UserEmail = er.User.Email,
                RegisteredAt = er.RegisteredAt
            });

            return Ok(registrationDtos);
        }

        // GET: api/registrations
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllRegistrations()
        {
            var registrations = await _context.EventRegistrations
                .Include(er => er.Event)
                .Include(er => er.User)
                .OrderByDescending(er => er.RegisteredAt)
                .ToListAsync();

            var registrationDtos = registrations.Select(er => new RegistrationDto
            {
                Id = er.Id,
                EventId = er.EventId,
                EventTitle = er.Event.Title,
                EventDate = er.Event.EventDate,
                EventLocation = er.Event.Location,
                UserId = er.UserId,
                Username = er.User.Username,
                UserEmail = er.User.Email,
                RegisteredAt = er.RegisteredAt
            });

            return Ok(registrationDtos);
        }

        // DELETE: api/registrations/admin/{id}
        [HttpDelete("admin/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AdminDeleteRegistration(int id)
        {
            var registration = await _context.EventRegistrations.FindAsync(id);
            if (registration == null)
                return NotFound("Registration not found.");

            _context.EventRegistrations.Remove(registration);
            await _context.SaveChangesAsync();

            return Ok("Registration deleted successfully by admin.");
        }
    }
}
