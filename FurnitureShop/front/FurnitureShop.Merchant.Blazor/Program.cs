using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FurnitureShop.Admin.Blazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

await builder.Build().RunAsync();
//https://codepen.io/aybukeceylan/pen/OJRNbZp
//https://codepen.io/michaelmcshinsky/pen/vYMdrb
//chart https://codepen.io/apexcharts/pen/pxZKqL
//https://codepen.io/jkantner/pen/mdLXQPq