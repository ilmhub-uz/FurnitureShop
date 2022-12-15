using FluentValidation.AspNetCore;
using FluentValidation;
using FurnitureShop.Common.Extensions;
using FurnitureShop.Common.Middleware;
using JFA.DependencyInjection;
using System.Reflection;
using Serilog;
using TelegramSink;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddCors();
builder.SerilogConfig();
builder.Services.AddServicesFromAttribute();
builder.Services.AddIdentityManagers();

var botToken = builder.Configuration.GetValue("BotToken", string.Empty);
var chatId = builder.Configuration.GetValue("ChatId", string.Empty);
var outlayTemplate = builder.Configuration.GetValue("outputTemplate.log", string.Empty);

var logger = new LoggerConfiguration()
    .WriteTo.TeleSink(botToken
        , chatId)
    .WriteTo.File(path: "path", rollingInterval: RollingInterval.Year,
        outputTemplate: outlayTemplate)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

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

app.Run();

