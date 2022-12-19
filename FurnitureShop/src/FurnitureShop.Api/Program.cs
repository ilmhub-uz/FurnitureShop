using FluentValidation;
using FurnitureShop.Common.Extensions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Middleware;
using JFA.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddCors();
builder.SerilogConfig();
builder.Services.AddServicesFromAttribute();
builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));

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
