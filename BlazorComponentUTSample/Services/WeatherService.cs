using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorComponentUTSample.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync();
    }
    public class WeatherService : IWeatherService
    {
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
        {
            await Task.Delay(1500);
            var forecasts = new List<WeatherForecast>();
            forecasts.Add(new WeatherForecast() { Date = new DateTime(2020, 5, 1), TemperatureC = 20, Summary = "Sunny" });
            forecasts.Add(new WeatherForecast() { Date = new DateTime(2020, 5, 2), TemperatureC = 10, Summary = "Rainy" });
            forecasts.Add(new WeatherForecast() { Date = new DateTime(2020, 5, 3), TemperatureC = 14, Summary = "Cloudy" });
            return forecasts;
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
