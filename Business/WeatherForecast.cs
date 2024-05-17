using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Business
{
    public class WeatherForecast
    {
        public DateTime DateTime { get; set; }
        [JsonPropertyName("temp_c")]
        public double TemperatureC { get; set; }
        [JsonPropertyName("precip_mm")]
        public double Precipitation { get; set; }
        [JsonPropertyName("chance_of_rain")]
        public int PrecipitationChance { get; set; }
        [JsonPropertyName("wind_kph")]
        public double WindSpeed { get; set; }
        [JsonPropertyName("wind_dir")]
        public double WindDirection { get; set; }
        [JsonPropertyName("text")]
        public string condition { get; set; }
    }
}
