// File: BloodDonationClient/Services/EventService.cs

using BloodDonationClient.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BloodDonationClient.Services
{
    public class EventService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;

        public EventService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
        }

        // Get all events
        public async Task<List<EventDto>> GetAllEventsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EventDto>>("api/events");
        }

        // Get event by ID
        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/events/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EventDto>();
            }
            else
            {
                throw new Exception("Failed to fetch event details.");
            }
        }

        // Create a new event (admin only)
        public async Task<bool> CreateEventAsync(EventCreateDto createDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/events", createDto);
            return response.IsSuccessStatusCode;
        }

        // Update an existing event (admin only)
        public async Task<bool> UpdateEventAsync(int id, EventUpdateDto updateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/events/{id}", updateDto);
            return response.IsSuccessStatusCode;
        }

        // Delete an event (admin only)
        public async Task<bool> DeleteEventAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/events/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
