using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.Domain.Dto
{
    public class WeatherDto
    {
        [Required]
        [DataType(DataType.Time)]
        public DateTime Date { get; set; }
        [Required]
        [Range(-100, 100)]
        public double Temperature { get; set; }
        [Required]
        [Range(0, 100)]
        public double RelativeHumidity { get; set; }
        [Required]
        [Range(-100, 100)]
        public double DewPoint { get; set; }
        [Required]
        [Range(500, 1000)]
        public int AtmosphericPressure { get; set; }
        public string WindDirection { get; set; }
        public int? WindSpeed { get; set; }
        [Range(0, 100)]
        public int? CloudCover { get; set; }
        public int? CloudLowerLimit { get; set; }
        public int? HorizontalVisibility { get; set; }
        public string WeatherPhenomena { get; set; }
    }
}
