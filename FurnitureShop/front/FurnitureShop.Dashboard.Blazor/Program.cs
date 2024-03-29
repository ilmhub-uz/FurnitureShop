using FurnitureShop.Common.Helpers;
using FurnitureShop.Dashboard.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:1003") });
builder.Services.AddMudServices();
builder.Services.AddTransient<RequestHelper>();
await builder.Build().RunAsync();
