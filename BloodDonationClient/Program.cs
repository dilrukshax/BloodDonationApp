// File: BloodDonationClient/Program.cs

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BloodDonationClient;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using BloodDonationClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient with the base address of the Web API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7252/") }); // Replace with your API's URL and port

// Add Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Add Authentication
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();



await builder.Build().RunAsync();

