using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComposeService
{
    public class WeatherForecast
    {
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecast>> GetWeather();
        Task<WeatherForecast> GetOneWeather();
    }
}