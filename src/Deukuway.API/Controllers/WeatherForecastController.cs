using Deukuway.Core.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Deukuway.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILoggerService _logger;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot",
        "Sweltering", "Scorching"
    };

    //private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILoggerService logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var properties = new Dictionary<string, object>
        {
            ["request"] = Summaries,
            ["userId"] = "user123" 
        };
        _logger.LogInfo("WeatherForecastController.Get", "Ge tWeatherForecast request",properties );

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}