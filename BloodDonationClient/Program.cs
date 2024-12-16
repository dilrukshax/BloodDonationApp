// File: BloodDonationClient/Program.cs

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BloodDonationClient;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using BloodDonationClient.Services;
using System;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register the AuthorizationMessageHandler
builder.Services.AddScoped<AuthorizationMessageHandler>();

// Configure HttpClient with the AuthorizationMessageHandler
builder.Services.AddHttpClient("BloodDonationAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/"); // Replace with your API's URL and port
})
.AddHttpMessageHandler<AuthorizationMessageHandler>();

// Register the HttpClient for dependency injection
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BloodDonationAPI"));

// Add Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Add Authentication
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

// Register AuthService
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();
