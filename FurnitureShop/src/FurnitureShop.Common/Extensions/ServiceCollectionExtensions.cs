using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace FurnitureShop.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static void GlobalAppSettings(this ConfigureWebHostBuilder webHost)
    {
        webHost.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
        {
            var pathOfcurrentDirectory = webHostBuilderContext.HostingEnvironment.ContentRootPath;
            var pathOfCommonSettingsFile = Path.GetFullPath(Path.Combine(pathOfcurrentDirectory, @"../" , "FurnitureShop.Common", "CommonSettings.json"));
            configurationBuilder
                .AddJsonFile(pathOfCommonSettingsFile, true, true);

            configurationBuilder.AddEnvironmentVariables();
            configurationBuilder.Build();
        });
    }
    public static void AddAppDbContext(this IServiceCollection collection, ConfigurationManager configuration)
    {
        collection.AddDbContext<AppDbContext>(options =>
        {
            options.UseLazyLoadingProxies().UseNpgsql(configuration.GetConnectionString("localhost"))
                .UseSnakeCaseNamingConvention();
        });
    }

    public static void AddIdentityManagers(this IServiceCollection collection)
    {
        collection.AddIdentity<AppUser, AppUserRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<AppDbContext>();

        collection.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Task.CompletedTask;
            };
        });
    }

    public static void AddCorsPolicy(this IServiceCollection collection)
    {
        collection.AddCors(options =>
        {
            options.AddDefaultPolicy(cors =>
            {
                cors.WithOrigins("http://localhost:65283",
                                "http://localhost:34445",
                                "https://localhost:44398",
                                "https://localhost:5001",
                                "https://localhost:5011",
                                "https://localhost:44398",
                                "http://localhost:5000",
                                "https://localhost:5001",
                                "https://localhost:0001",   // Marketplace API HTTPS
                                "http://localhost:0002",    // Marketplace API HTTP
                                "https://localhost:0003",   // Admin API HTTPS
                                "http://localhost:0004",    // Admin API HTTP
                                "https://localhost:0005",   // Client API HTTPS
                                "http://localhost:0006",    // Client API HTTP
                                "https://localhost:1007",   // Files API HTTPS
                                "http://localhost:1008",    // Files API HTTP
                                "https://localhost:1009",   // Merchant API HTTPS
                                "http://localhost:1010")    // Merchant API HTTP
                                "https://localhost:0007",   // Files API HTTPS
                                "http://localhost:0008",    // Files API HTTP
                                "https://localhost:0009",   // Merchant API HTTPS
                                "http://localhost:0010")    // Merchant API HTTP
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }
}