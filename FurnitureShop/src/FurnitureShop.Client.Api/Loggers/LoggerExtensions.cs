using Serilog;
using TelegramSink;

namespace FurnitureShop.Client.Api.Loggers;

public static class LoggerExtensions
{
    public static void SerilogConfig(this WebApplicationBuilder builder)
    {
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
    }
}
