using FluentValidation;
using FurnitureShop.Common.Extensions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Middleware;
using JFA.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Reflection;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddCorsPolicy();
builder.SerilogConfig();

builder.Services.AddServicesFromAttribute();
builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));

builder.Services.AddIdentityManagers();

var app = builder.Build();



if (((IApplicationBuilder)app).ApplicationServices.GetService<IHttpContextAccessor>() != null)
    HttpContextHelper.Accessor =
        ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IHttpContextAccessor>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandlerMiddleware();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
