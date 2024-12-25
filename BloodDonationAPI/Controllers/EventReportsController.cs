// File: BloodDonationAPI/Controllers/EventReportsController.cs

using BloodDonationAPI.Data;
using BloodDonationAPI.DTOs;
using BloodDonationAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")] // Ensure only admins can access
    public class EventReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/eventreports/{eventId}
        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventReport(int eventId)
        {
            var report = await _context.EventReports
                .Include(er => er.Event)
                .FirstOrDefaultAsync(er => er.EventId == eventId);

            if (report == null)
                return NotFound("Report not found for the specified event.");

            var reportDto = new EventReportDto
            {
                EventId = report.EventId,
                EventTitle = report.Event.Title,
                ParticipatedPeopleCount = report.ParticipatedPeopleCount,
                Cost = report.Cost,
                APositive = report.APositive,
                BPositive = report.BPositive,
                ABPositive = report.ABPositive,
                OPositive = report.OPositive,
                ANegative = report.ANegative,
                BNegative = report.BNegative,
                ABNegative = report.ABNegative,
                ONegative = report.ONegative
            };

            return Ok(reportDto);
        }

        // GET: api/eventreports
        [HttpGet]
        public async Task<IActionResult> GetAllEventReports()
        {
            var reports = await _context.EventReports
                .Include(er => er.Event)
                .ToListAsync();

            var reportDtos = reports.Select(report => new EventReportDto
            {
                EventId = report.EventId,
                EventTitle = report.Event.Title,
                ParticipatedPeopleCount = report.ParticipatedPeopleCount,
                Cost = report.Cost,
                APositive = report.APositive,
                BPositive = report.BPositive,
                ABPositive = report.ABPositive,
                OPositive = report.OPositive,
                ANegative = report.ANegative,
                BNegative = report.BNegative,
                ABNegative = report.ABNegative,
                ONegative = report.ONegative
            }).ToList();

            return Ok(reportDtos);
        }

        // POST: api/eventreports
        [HttpPost]
        public async Task<IActionResult> CreateEventReport([FromBody] EventReportCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the event exists
            var eventExists = await _context.Events.AnyAsync(e => e.Id == createDto.EventId);
            if (!eventExists)
                return NotFound("Event not found.");

            // Check if a report already exists for the event
            var existingReport = await _context.EventReports.AnyAsync(er => er.EventId == createDto.EventId);
            if (existingReport)
                return BadRequest("A report already exists for this event.");

            var report = new EventReport
            {
                EventId = createDto.EventId,
                ParticipatedPeopleCount = createDto.ParticipatedPeopleCount,
                Cost = createDto.Cost,
                APositive = createDto.APositive,
                BPositive = createDto.BPositive,
                ABPositive = createDto.ABPositive,
                OPositive = createDto.OPositive,
                ANegative = createDto.ANegative,
                BNegative = createDto.BNegative,
                ABNegative = createDto.ABNegative,
                ONegative = createDto.ONegative
            };

            _context.EventReports.Add(report);
            await _context.SaveChangesAsync();

            var reportDto = new EventReportDto
            {
                EventId = report.EventId,
                EventTitle = (await _context.Events.FindAsync(report.EventId)).Title,
                ParticipatedPeopleCount = report.ParticipatedPeopleCount,
                Cost = report.Cost,
                APositive = report.APositive,
                BPositive = report.BPositive,
                ABPositive = report.ABPositive,
                OPositive = report.OPositive,
                ANegative = report.ANegative,
                BNegative = report.BNegative,
                ABNegative = report.ABNegative,
                ONegative = report.ONegative
            };

            return CreatedAtAction(nameof(GetEventReport), new { eventId = report.EventId }, reportDto);
        }

        // PUT: api/eventreports/{eventId}
        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateEventReport(int eventId, [FromBody] EventReportUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var report = await _context.EventReports.FirstOrDefaultAsync(er => er.EventId == eventId);
            if (report == null)
                return NotFound("Report not found for the specified event.");

            // Update properties
            report.ParticipatedPeopleCount = updateDto.ParticipatedPeopleCount;
            report.Cost = updateDto.Cost;
            report.APositive = updateDto.APositive;
            report.BPositive = updateDto.BPositive;
            report.ABPositive = updateDto.ABPositive;
            report.OPositive = updateDto.OPositive;
            report.ANegative = updateDto.ANegative;
            report.BNegative = updateDto.BNegative;
            report.ABNegative = updateDto.ABNegative;
            report.ONegative = updateDto.ONegative;

            _context.EventReports.Update(report);
            await _context.SaveChangesAsync();

            return Ok("Event report updated successfully.");
        }

        // DELETE: api/eventreports/{eventId}
        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEventReport(int eventId)
        {
            var report = await _context.EventReports.FirstOrDefaultAsync(er => er.EventId == eventId);
            if (report == null)
                return NotFound("Report not found for the specified event.");

            _context.EventReports.Remove(report);
            await _context.SaveChangesAsync();

            return Ok("Event report deleted successfully.");
        }
    }
}
