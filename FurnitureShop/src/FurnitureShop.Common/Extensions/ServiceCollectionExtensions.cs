using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Net;
using System.Reflection;

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
                cors.WithOrigins("http://localhost:65283", // Merchant.Api
                                "https://localhost:8521", 
                                "http://localhost:9842", 
                                "http://localhost:34445", // File.Api
                                "https://localhost:7019",
                                "http://localhost:5285",
                                "https://localhost:44398",
                                "https://localhost:5001",
                                "https://localhost:5011",
                                "https://localhost:44398")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }
}