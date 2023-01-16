using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FurnitureShop.Merchant.Blazor;
using MudBlazor.Services;
using FurnitureShop.Merchant.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });   // "https://localhost:1009"
builder.Services.AddScoped<OrganizationService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
