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
            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
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
                cors.WithOrigins("https://localhost:1001",   // Marketplace API HTTPS
                                "https://localhost:2001",   // Marketplace Blazor HTTPS
                                "http://localhost:1002",    // Marketplace API HTTP
                                "http://localhost:2002",    // Marketplace Blazor HTTP
                                "https://localhost:1003",   // Admin API HTTPS
                                "https://localhost:2003",   // Admin Blazor HTTPS
                                "http://localhost:1004",    // Admin API HTTP
                                "http://localhost:2004",    // Admin Blazor HTTP
                                "https://localhost:1005",   // Client API HTTPS
                                "https://localhost:2005",   // Client Blazor HTTPS
                                "http://localhost:1006",    // Client API HTTP
                                "http://localhost:2006",    // Client Blazor HTTP
                                "https://localhost:1007",   // Files API HTTPS
                                "https://localhost:2007",   // Files Blazor HTTPS
                                "http://localhost:1008",    // Files API HTTP
                                "http://localhost:2008",    // Files Blazor HTTP
                                "https://localhost:1009",   // Merchant API HTTPS
                                "https://localhost:2009",   // Merchant Blazor HTTPS
                                "http://localhost:1010",    // Merchant API HTTP
                                "http://localhost:2010",    // Merchant Blazor HTTP
                                "https://localhost:7777",    // Email Sender Api HTTPS
                                "http://localhost:2001",    // Email Sender Api HTTPS
                                "http://localhost:2003",    // Email Sender Api HTTPS
                                "http://localhost:2005",    // Email Sender Api HTTPS
                                "http://localhost:2009",    // Email Sender Api HTTPS
                                "http://localhost:7778")    // Email Sender Api HTTP
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }
}