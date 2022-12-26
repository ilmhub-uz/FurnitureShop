using FluentValidation;
using FluentValidation.AspNetCore;
using FurnitureShop.Common.Extensions;
using JFA.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddCorsPolicy();
builder.SerilogConfig();
builder.Services.AddServicesFromAttribute();

builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));
builder.Services.AddFluentValidationAutoValidation(o =>
{
    o.DisableDataAnnotationsValidation = true;
});
builder.Services.AddIdentityManagers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
