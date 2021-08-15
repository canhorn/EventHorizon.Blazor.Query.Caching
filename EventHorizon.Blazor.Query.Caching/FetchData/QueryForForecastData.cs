namespace EventHorizon.Blazor.Query.Caching.FetchData
{
    using System.Collections.Generic;

    using EventHorizon.Cache;

    using MediatR;

    [GenerateCache]
    public struct QueryForForecastData
        : IRequest<IEnumerable<WeatherForecast>>,
        CacheKey
    {
        // A prefix that we can use to purge a whole section of the cache.
        public string CacheKeyPrefix => "fetchdata";
        // A "dynamic" cache key, we can use this to create custom keys if we want.
        public string CacheKey => $"{CacheKeyPrefix}:{nameof(QueryForForecastData)}";
    }
}
