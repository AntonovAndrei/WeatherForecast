using System;

namespace WeatherForecast.ViewModels
{
    public class WeatherViewModel
    {
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double RelativeHumidity { get; set; }
        public double DewPoint { get; set; }
        public int AtmosphericPressure { get; set; }
        public string WindDirection { get; set; }
        public int? WindSpeed { get; set; }
        public int? CloudCover { get; set; }
        public int? CloudLowerLimit { get; set; }
        public string HorizontalVisibility { get; set; }
        public string WeatherPhenomena { get; set; }
    }
}
