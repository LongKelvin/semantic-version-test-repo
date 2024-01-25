using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

namespace Semantic_Versioning_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
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
    }
}