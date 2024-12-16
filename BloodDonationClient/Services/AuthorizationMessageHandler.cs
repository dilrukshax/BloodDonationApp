using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BloodDonationClient.Services
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigation;

        public AuthorizationMessageHandler(ILocalStorageService localStorage, NavigationManager navigation)
        {
            _localStorage = localStorage;
            _navigation = navigation;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
