

using System;
using System.Collections.Generic;
using WeatherForecast.Domain.Dto;
using WeatherForecast.Domain.Repositories.Abstract;

namespace WeatherForecast.Domain.Repositories.EntityFramework
{
    public class EFWeatherRepository : IWeatherRepository
    {
        public void AddWeather(IEnumerable<WeatherDto> weatherDtos)
        {
            
        }

        public IEnumerable<WeatherDto> GetWeatherByWeek(DateTime monthBeginning)
        {
            throw new NotImplementedException();
        }
    }
}
