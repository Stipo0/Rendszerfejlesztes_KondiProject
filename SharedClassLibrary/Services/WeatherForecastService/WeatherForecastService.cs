using SharedLibrary.Models;
using System.Net.Http.Json;

namespace SharedLibrary.Services.WeatherForecastService
{
	public class WeatherForecastService : IWeatherForecastService
	{
		private readonly HttpClient _http;
        public WeatherForecastService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<WeatherForecast>?> GetWeather()
		{
			return await _http.GetFromJsonAsync<List<WeatherForecast>>("api/WeatherForecast");
		}
	}
}
