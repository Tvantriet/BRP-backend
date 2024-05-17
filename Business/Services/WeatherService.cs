using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;

namespace Business.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string? ApiKey;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            ApiKey = _configuration["KEYS:WeatherAPI"];
        }
        public async Task<WeatherForecast> GetWeatherForecastAsync(double lon, double lat, DateTime dateTime)
        {
            string lonString = lon.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string latString = lat.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string apiUrl = $"https://api.weatherapi.com/v1/forecast.json?q={lonString},{latString}&days=1&hour={dateTime.Hour}&alerts=yes&key={ApiKey}";

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                WeatherForecast forecast = jsonToForecast(json);


                return forecast;
            }
            else
            {
                throw new HttpRequestException($"API request failed with status code {response.StatusCode}");
            }
        }
        public WeatherForecast? jsonToForecast(string jsonS)
        {
            JObject json = JObject.Parse(jsonS);
            JToken ForecastHour = json["forecast"]["forecastday"][0]["hour"];
            WeatherForecast forecast = new WeatherForecast();
            forecast.DateTime = DateTime.Parse(ForecastHour[0]["time"].ToString());
            forecast.TemperatureC = double.Parse(ForecastHour[0]["temp_c"].ToString());
            forecast.Precipitation = Convert.ToDouble(ForecastHour[0]["precip_mm"].ToString());
            forecast.PrecipitationChance = int.Parse(ForecastHour[0]["chance_of_rain"].ToString());
            forecast.WindSpeed = double.Parse(ForecastHour[0]["wind_kph"].ToString());
            forecast.WindDirection = double.Parse(ForecastHour[0]["wind_degree"].ToString());
            forecast.condition = ForecastHour[0]["condition"]["text"].ToString();

            return forecast;
        }
    }
}
