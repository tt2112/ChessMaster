using System;
using System.Threading.Tasks;
using ChessMaster.WebApp.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace ChessMaster.WebApp;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            var app = WebApplication.CreateBuilder(args)
                                    .UseSerilog()
                                    .ConfigureDependencyInjectionContainer()
                                    .Build()
                                    .ConfigureHttpPipeline();


            await app.RunAsync();
            return 0;
        }
        catch (Exception exception)
        {
            var logger = Logging.GetStartupEmergencyLogger();
            logger.Error(exception, "Failed to start application");
            return -1;
        }
    }
}