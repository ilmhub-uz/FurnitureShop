using FluentValidation.AspNetCore;
using FluentValidation;
using FurnitureShop.Common.Extensions;
using FurnitureShop.Common.Middleware;
using JFA.DependencyInjection;
using System.Reflection;
using FurnitureShop.Client.Api.Services;
using FurnitureShop.Client.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAppDbContext(builder.Configuration);

builder.Services.AddSingleton<HubClientsService>();

builder.Services.AddSignalR();
builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(corsPolicy =>
    {
        corsPolicy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});
builder.Services.AddCors();
builder.SerilogConfig();
builder.Services.AddServicesFromAttribute();
builder.Services.AddIdentityManagers();

builder.Services.AddServicesFromAttribute();

builder.Services.AddFluentValidationAutoValidation(o =>
{
    o.DisableDataAnnotationsValidation = false;
});

builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));

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

app.MapHub<MessagesHub>("/messages");

app.Run();