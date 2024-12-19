// File: BloodDonationAPI/Controllers/EventsController.cs

using BloodDonationAPI.Data;
using BloodDonationAPI.DTOs;
using BloodDonationAPI.Models;
using BloodDonationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // All endpoints require authentication
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public EventsController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: api/events
        [HttpGet]
        [AllowAnonymous] // Allow unauthenticated users to view events
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _context.Events.OrderByDescending(e => e.EventDate).ToListAsync();

            var eventDtos = events.Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                EventDate = e.EventDate,
                Location = e.Location,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            });

            return Ok(eventDtos);
        }

        // GET: api/events/{id}
        [HttpGet("{id}")]
        [AllowAnonymous] // Allow unauthenticated users to view events
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null)
                return NotFound("Event not found.");

            var eventDto = new EventDto
            {
                Id = eventEntity.Id,
                Title = eventEntity.Title,
                Description = eventEntity.Description,
                EventDate = eventEntity.EventDate,
                Location = eventEntity.Location,
                CreatedAt = eventEntity.CreatedAt,
                UpdatedAt = eventEntity.UpdatedAt
            };

            return Ok(eventDto);
        }

        // POST: api/events
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newEvent = new Event
            {
                Title = createDto.Title,
                Description = createDto.Description,
                EventDate = createDto.EventDate,
                Location = createDto.Location,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            // Send email to all users about the new event
            var users = await _context.Users.ToListAsync();
            var subject = "New Blood Donation Event Announced!";
            var body = $@"
                <h1>{newEvent.Title}</h1>
                <p>{newEvent.Description}</p>
                <p><strong>Date:</strong> {newEvent.EventDate.ToString("f")}</p>
                <p><strong>Location:</strong> {newEvent.Location}</p>
                <p>We hope to see you there!</p>
                <p>Best Regards,<br/>Blood Donation Team</p>
            ";

            var emailTasks = users.Select(user =>
                _emailService.SendEmailAsync(user.Email, subject, body)
            );

            try
            {
                await Task.WhenAll(emailTasks);
            }
            catch (Exception ex)
            {
                // Log the exception, but do not fail the request
                // Implement logging as needed
            }

            var eventDto = new EventDto
            {
                Id = newEvent.Id,
                Title = newEvent.Title,
                Description = newEvent.Description,
                EventDate = newEvent.EventDate,
                Location = newEvent.Location,
                CreatedAt = newEvent.CreatedAt,
                UpdatedAt = newEvent.UpdatedAt
            };

            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.Id }, eventDto);
        }

        // PUT: api/events/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null)
                return NotFound("Event not found.");

            bool isUpdated = false;

            if (!string.IsNullOrWhiteSpace(updateDto.Title))
            {
                eventEntity.Title = updateDto.Title;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.Description))
            {
                eventEntity.Description = updateDto.Description;
                isUpdated = true;
            }

            if (updateDto.EventDate.HasValue)
            {
                eventEntity.EventDate = updateDto.EventDate.Value;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.Location))
            {
                eventEntity.Location = updateDto.Location;
                isUpdated = true;
            }

            if (isUpdated)
            {
                eventEntity.UpdatedAt = DateTime.UtcNow;
                _context.Events.Update(eventEntity);
                await _context.SaveChangesAsync();
            }

            return Ok("Event updated successfully.");
        }

        // DELETE: api/events/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null)
                return NotFound("Event not found.");

            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync();

            return Ok("Event deleted successfully.");
        }
    }
}
