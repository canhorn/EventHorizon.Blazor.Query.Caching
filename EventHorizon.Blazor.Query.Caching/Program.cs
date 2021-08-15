namespace EventHorizon.Blazor.Query.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using EventHorizon.Blazor.Query.Caching.FetchData;

    using MediatR;

    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services
                .AddScoped(
                sp => new HttpClient
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                }
            );

            builder.Services.AddEasyCaching(
                option =>
                {
                    option.UseInMemory(
                        options =>
                        {
                            // We decrease these number from the defaults so we can see the Cache Hit/Miss in action
                            // Our expiration for our Cache Generator is 30 seconds
                            options.EnableLogging = true;
                            options.MaxRdSecond = 5; // Default 120
                            options.DBConfig.ExpirationScanFrequency = 10; // Default 60
                        }
                    );
                }
            );

            builder.Services.AddMediatR(
                typeof(Program)
            );

            builder.Services
                // Here we are registering our cache behavior.
                // This is intentionally a manual process so we can disable it at a fine grain level.
                .AddScoped<IPipelineBehavior<QueryForForecastData, IEnumerable<WeatherForecast>>, QueryForForecastDataGeneratedCachedBehavior>();

            await builder.Build().RunAsync();
        }
    }
}
