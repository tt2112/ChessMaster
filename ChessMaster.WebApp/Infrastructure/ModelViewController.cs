using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ChessMaster.WebApp.Infrastructure;

public static class ModelViewController
{
    public static IServiceCollection AddModelViewController(this IServiceCollection services)
    {
        services.AddMvc()
                .AddControllersAsServices();

        return services;
    }

    public static IApplicationBuilder UseModelViewControllerEndpoints(this IApplicationBuilder app) =>
        app.UseEndpoints(endpoints => endpoints.MapControllers());
}