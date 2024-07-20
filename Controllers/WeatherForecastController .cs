using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drivers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "appUser")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly List<WeatherForecast> Forecasts = new List<WeatherForecast>
        {
            new WeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(1)), 28, "Warm"),
            new WeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(2)), 22, "Sweltering"),
            new WeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(3)), 1, "Freezing"),
            new WeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(4)), 48, "Cool"),
            new WeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(5)), 49, "Freezing")
        };

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Forecasts;
        }

        //to post something we need to have the claim WeatherDataAdmins (just the user with : WeatherAdmins value claim)
        [Authorize(Policy = "WeatherDataAdmins")]
        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecast newForecast)
        {
            if (newForecast == null)
            {
                return BadRequest("Invalid forecast data.");
            }

            Forecasts.Add(newForecast);
            return Ok(newForecast);
        }

    }

    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

}