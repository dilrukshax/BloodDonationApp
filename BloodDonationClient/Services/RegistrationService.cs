// File: BloodDonationClient/Services/RegistrationService.cs

using BloodDonationClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BloodDonationClient.Services
{
    public class RegistrationService
    {
        private readonly HttpClient _httpClient;

        public RegistrationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Register for an event
        public async Task<bool> RegisterForEventAsync(int eventId)
        {
            var createDto = new CreateRegistrationDto
            {
                EventId = eventId
            };

            var response = await _httpClient.PostAsJsonAsync("api/registrations", createDto);
            return response.IsSuccessStatusCode;
        }

        // Get user's registrations
        public async Task<List<RegistrationDto>> GetMyRegistrationsAsync()
        {
            var response = await _httpClient.GetAsync("api/registrations/my");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<RegistrationDto>>();
            }
            else
            {
                throw new Exception("Failed to fetch your registrations.");
            }
        }

        // Get all registrations (admin)
        public async Task<List<RegistrationDto>> GetAllRegistrationsAsync()
        {
            var response = await _httpClient.GetAsync("api/registrations");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<RegistrationDto>>();
            }
            else
            {
                throw new Exception("Failed to fetch all registrations.");
            }
        }

        // Delete a registration (user)
        public async Task<bool> DeleteRegistrationAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/registrations/{id}");
            return response.IsSuccessStatusCode;
        }

        // Delete a registration (admin)
        public async Task<bool> AdminDeleteRegistrationAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/registrations/admin/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
