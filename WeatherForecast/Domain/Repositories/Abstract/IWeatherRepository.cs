using System.Collections.Generic;
using WeatherForecast.Domain.Dto;

namespace WeatherForecast.Domain.Repositories.Abstract
{
    public interface IWeatherRepository
    {
        IEnumerable<WeatherDto> GetWeatherByWeek(Date)
    }
}
