using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace BloodDonationClient.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { Email = email, Password = password });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                await _localStorage.SetItemAsync("authToken", result.Token);
                return true;
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
        }

        private class LoginResponse
        {
            public string Token { get; set; }
        }
    }
}
