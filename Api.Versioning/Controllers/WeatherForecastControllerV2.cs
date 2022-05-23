using Microsoft.AspNetCore.Mvc;

namespace Api.Versioning.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecastV2> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastV2
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                IsGoodWeather = Random.Shared.NextDouble() > 0.5 //generate random bool value
            })
            .ToArray();
        }
    }
}
