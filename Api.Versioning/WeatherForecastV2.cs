﻿namespace Api.Versioning
{
    public class WeatherForecastV2
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public bool IsGoodWeather { get; set; }
    }
}
