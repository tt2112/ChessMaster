using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ChessMaster.WebApp.Infrastructure;

public static class HttpPipeline
{
    public static WebApplication ConfigureHttpPipeline(this WebApplication app)
    {
        var configuration = app.Configuration;
        var environment = app.Environment;

        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage()
               .UseCorsIfNecessary(configuration);
        }

        app.UseSerilogRequestLogging()
           .UseHttpsRedirection()
           .UseRewriter(new RewriteOptions().AddRedirectToWww())
           .UseRouting()
           .UseAuthorization()
           .UseModelViewControllerEndpoints()
           .UseSpaStaticFilesIfNecessary(environment)
           .UseSpaIfNecessary(environment);

        return app;
    }
}