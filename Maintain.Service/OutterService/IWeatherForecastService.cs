using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComposeService
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecast>> GetWeather();

        Task<WeatherForecast> GetOneWeather();
    }
}