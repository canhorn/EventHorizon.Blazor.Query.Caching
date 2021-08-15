namespace EventHorizon.Blazor.Query.Caching.FetchData
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    public class QueryForForecastDataHandler
        : IRequestHandler<QueryForForecastData, IEnumerable<WeatherForecast>>
    {
        private readonly HttpClient _httpClient;

        public QueryForForecastDataHandler(
            HttpClient httpClient
        )
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WeatherForecast>> Handle(
            QueryForForecastData request,
            CancellationToken cancellationToken
        )
        {
            return await _httpClient.GetFromJsonAsync<List<WeatherForecast>>(
                "sample-data/weather.json",
                cancellationToken: cancellationToken
            ) ?? new List<WeatherForecast>();
        }
    }
}
