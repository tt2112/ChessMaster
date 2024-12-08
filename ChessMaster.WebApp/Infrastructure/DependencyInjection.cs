using Light.SharedCore.Time;
using LightInject.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace ChessMaster.WebApp.Infrastructure;

public static class DependencyInjection
{
    public static WebApplicationBuilder ConfigureDependencyInjectionContainer(this WebApplicationBuilder builder)
    {
        builder.Host.UseServiceProviderFactory(new LightInjectServiceProviderFactory());
        return builder.ConfigureServices();
    }

    private static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        var environment = builder.Environment;
        services.AddCorsIfNecessary(environment)
                .AddModelViewController()
                .AddSpaStaticFilesIfNecessary(environment)
                .AddUtcClock();
        return builder;
    }
}