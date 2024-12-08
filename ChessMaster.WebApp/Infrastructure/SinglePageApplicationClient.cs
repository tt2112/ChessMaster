using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChessMaster.WebApp.Infrastructure;

public static class SinglePageApplicationClient
{
    private const string WebClientDirectory = "ChessMaster.Client";

    public static IServiceCollection AddSpaStaticFilesIfNecessary(this IServiceCollection services, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
            return services;

        services.AddSpaStaticFiles(options => options.RootPath = WebClientDirectory);
        return services;
    }

    public static IApplicationBuilder UseSpaStaticFilesIfNecessary(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if(environment.IsDevelopment())
            return app;

        app.UseSpaStaticFiles();
        return app;
    }


    public static IApplicationBuilder UseSpaIfNecessary(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if(environment.IsDevelopment())
            return app;

        app.UseSpa(spaBuilder => spaBuilder.Options.SourcePath = WebClientDirectory);
        return app;
    }
}