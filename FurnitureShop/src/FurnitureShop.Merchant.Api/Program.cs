using FluentValidation;
using FurnitureShop.Common.Extensions;
using FurnitureShop.Common.Middleware;
using FurnitureShop.Merchant.Api.Hubs;
using JFA.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//builder.Services.AddSingleton(emailConfig);
builder.WebHost.GlobalAppSettings();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddCorsPolicy();
builder.SerilogConfig();
builder.Services.AddServicesFromAttribute();
builder.Services.AddIdentityManagers();
builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));
builder.Services.AddSignalR();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseErrorHandlerMiddleware();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//https://localhost:7113/organizationhub
app.MapHub<OrganizationHub>("/organizationhub");

app.Run();