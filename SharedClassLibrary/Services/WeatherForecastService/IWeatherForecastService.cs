using SharedLibrary.Models;

namespace SharedLibrary.Services.WeatherForecastService
{
    public interface IWeatherForecastService
    {
		Task<List<WeatherForecast>?> GetWeather();
	}
}
