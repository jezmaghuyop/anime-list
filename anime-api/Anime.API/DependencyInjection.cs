using Anime.API.Hubs;
using Anime.API.Infrastructure;
using Anime.API.Services;
using Anime.API.Subscriptions;
using Anime.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddSignalR();

        services.AddDatabaseDeveloperPageExceptionFilter();
        
        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();
        
        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();
        
        services.AddSingleton<AnimeVotesSubscription>();        
        services.AddScoped<IAnimeService, AnimeService>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                // Angular app's origin
                builder.WithOrigins("http://localhost:4200") 
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowCredentials(); // Allow credentials
            });
        });
        
        return services;
    }


    public static void UseSqlTableDependency<T>(this IApplicationBuilder services, string connectionString)
  where T : IDatabaseSubscription
    {
        var serviceProvider = services.ApplicationServices;
        var subscription = serviceProvider.GetService<T>();
        subscription?.SubscribeTableDependency(connectionString);
    }
}