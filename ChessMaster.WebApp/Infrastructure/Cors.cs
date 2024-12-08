using Light.GuardClauses;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChessMaster.WebApp.Infrastructure;

public static class Cors
{
    public static IServiceCollection AddCorsIfNecessary(this IServiceCollection services, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
            services.AddCors();

        return services;
    }

    public static IApplicationBuilder UseCorsIfNecessary(this IApplicationBuilder app, IConfiguration configuration)
    {
        var allowedCors = configuration.GetSection("allowedCors").Get<string[]>();
        if (allowedCors.IsNullOrEmpty())
            return app;

        app.UseCors(builder => builder.WithOrigins(allowedCors)
                                      .AllowAnyHeader()
                                      .AllowCredentials()
                                      .AllowAnyMethod());

        return app;
    }
}