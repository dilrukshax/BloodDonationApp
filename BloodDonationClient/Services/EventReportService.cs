// File: BloodDonationClient/Services/EventReportService.cs

using BloodDonationClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BloodDonationClient.Services
{
    public class EventReportService
    {
        private readonly HttpClient _httpClient;

        public EventReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all event reports
        public async Task<List<EventReportDto>> GetAllEventReportsAsync()
        {
            var response = await _httpClient.GetAsync("api/EventReports");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<EventReportDto>>();
            }
            else
            {
                throw new Exception("Failed to fetch event reports.");
            }
        }

        // Get a specific event report by EventId
        public async Task<EventReportDto> GetEventReportByEventIdAsync(int eventId)
        {
            var response = await _httpClient.GetAsync($"api/EventReports/{eventId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EventReportDto>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new Exception("Failed to fetch the event report.");
            }
        }

        // Create a new event report
        public async Task<bool> CreateEventReportAsync(EventReportCreateDto createDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/EventReports", createDto);
            return response.IsSuccessStatusCode;
        }

        // Update an existing event report
        public async Task<bool> UpdateEventReportAsync(int eventId, EventReportUpdateDto updateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/EventReports/{eventId}", updateDto);
            return response.IsSuccessStatusCode;
        }

        // Delete an event report
        public async Task<bool> DeleteEventReportAsync(int eventId)
        {
            var response = await _httpClient.DeleteAsync($"api/EventReports/{eventId}");
            return response.IsSuccessStatusCode;
        }

        // Get event summary (new method)
        public async Task<EventSummaryDto> GetEventSummaryAsync()
        {
            var response = await _httpClient.GetAsync("api/EventReports/summary");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EventSummaryDto>();
            }
            else
            {
                throw new Exception("Failed to fetch the event summary.");
            }
        }
    }
}
