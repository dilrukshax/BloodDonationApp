// File: BloodDonationClient/Program.cs

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BloodDonationClient;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using BloodDonationClient.Services;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Add Authorization
builder.Services.AddAuthorizationCore();

// Register ApiAuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

// Register AuthorizationMessageHandler
builder.Services.AddScoped<AuthorizationMessageHandler>();

// Configure HttpClient with the AuthorizationMessageHandler
builder.Services.AddHttpClient("BloodDonationAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/"); // Replace with your API's URL and port
})
.AddHttpMessageHandler<AuthorizationMessageHandler>();

// Register the named HttpClient for dependency injection
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BloodDonationAPI"));

// Register AuthService, AdminService, EventService, and RegistrationService
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<RegistrationService>();
builder.Services.AddScoped<EventReportService>();

var host = builder.Build();

// Initialize the AuthenticationStateProvider with the token from LocalStorage
var localStorage = host.Services.GetRequiredService<ILocalStorageService>();
var authStateProvider = host.Services.GetRequiredService<AuthenticationStateProvider>() as ApiAuthenticationStateProvider;
var token = await localStorage.GetItemAsync<string>("authToken");
if (!string.IsNullOrWhiteSpace(token))
{
    authStateProvider.NotifyUserAuthentication(token);
    host.Services.GetRequiredService<HttpClient>().DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
}

await host.RunAsync();