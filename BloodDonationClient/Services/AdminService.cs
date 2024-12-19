using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BloodDonationClient.Models;

namespace BloodDonationClient.Services
{
    public class AdminService
    {
        private readonly HttpClient _httpClient;

        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all users
        public async Task<List<AdminUserDto>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("api/admin/users");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<AdminUserDto>>();
        }

        // Get user by ID
        public async Task<AdminUserDto> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/admin/users/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AdminUserDto>();
        }

        // Update user
        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/admin/users/{id}", updateUserDto);
            return response.IsSuccessStatusCode;
        }

        // Delete user
        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/admin/users/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
