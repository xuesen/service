using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComposeService
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient _httpClient;

        public WeatherForecastService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeather()
        {
            var responseString = await _httpClient.GetStringAsync("weatherforecast");

            try
            {
                var catalog = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(responseString);
                return catalog;
            }
            catch (System.Exception)
            {
                return new List<WeatherForecast>() { new WeatherForecast { TemperatureC = 0, Summary = responseString } };
            }
        }

        public async Task<WeatherForecast> GetOneWeather()
        {
            var responseString = await _httpClient.GetStringAsync("weatherforecast/one");

            try
            {
                var catalog = JsonConvert.DeserializeObject<WeatherForecast>(responseString);
                return catalog;
            }
            catch (System.Exception)
            {
                return new WeatherForecast { TemperatureC = 0, Summary = responseString };
            }
        }
    }
}