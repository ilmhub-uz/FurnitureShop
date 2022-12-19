using FluentValidation;
using FluentValidation.AspNetCore;
using FurnitureShop.Common.Extensions;
using JFA.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddCors();
builder.SerilogConfig();
builder.Services.AddServicesFromAttribute();

builder.Services.AddFluentValidationAutoValidation(o =>
{
    o.DisableDataAnnotationsValidation = false;
});
builder.Services.AddIdentityManagers();

builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
