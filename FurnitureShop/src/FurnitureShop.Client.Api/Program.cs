using FluentValidation.AspNetCore;
using FluentValidation;
using FurnitureShop.Common.Extensions;
using FurnitureShop.Common.Middleware;
using JFA.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.GlobalAppSettings();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCorsPolicy();
builder.SerilogConfig();
builder.Services.AddServicesFromAttribute();
builder.Services.AddIdentityManagers();
builder.Services.AddServicesFromAttribute();
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddFluentValidationAutoValidation(o =>
{
    o.DisableDataAnnotationsValidation = true;
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

app.Run();