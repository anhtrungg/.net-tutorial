using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace SerilogDemo.Controllers
{
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
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Weather Forecast");

            var position = new { Latitude = 25, Longitude = 134 };
            var elapsedMs = 34;

            _logger.LogInformation("Processed {@Position} in {Elapsed:000} ms.", position, elapsedMs);

            using (var log = new LoggerConfiguration().WriteTo.Console().CreateLogger()) 
            {
                log.Information("Hello, Serilog");
                log.Warning("Goodbye, Serilog");
            };

                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}