using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

namespace Semantic_Versioning_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private static WeatherForecast[]? _data;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;

            using StreamReader reader = new("weather_forecast.json");
            var json = reader.ReadToEnd();

            // Deserialize JSON data into a list of WeatherForecast objects
            var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(json)!;

            _data = forecasts ?? [];
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast>? Get()
        {
            // Read JSON data from file
            using StreamReader reader = new("weather_forecast.json");
            var json = reader.ReadToEnd();

            // Deserialize JSON data into a list of WeatherForecast objects
            WeatherForecast[] forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(json)!;

            return forecasts?.ToArray();
        }

        [HttpGet("ByKey", Name = "GetWeatherForecastByKey")]
        public List<WeatherForecast> GetByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return [.. _data!];

            var queryResult = _data!.Where(x => x.Summary!.ToLower()!.Contains(key, StringComparison.CurrentCultureIgnoreCase) ||
            x.Location!.ToLower()!.Contains(key, StringComparison.CurrentCultureIgnoreCase)).ToList();
            return queryResult;
        }
    }
}