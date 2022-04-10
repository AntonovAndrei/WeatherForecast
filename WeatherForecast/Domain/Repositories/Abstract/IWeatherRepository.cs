using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Domain.Dto;

namespace WeatherForecast.Domain.Repositories.Abstract
{
    public interface IWeatherRepository
    {
        public Task<IEnumerable<WeatherDto>> GetWeatherByWeekAsync(DateTime monthBeginning);
        public Task AddWeatherAsync(IEnumerable<WeatherDto> weatherDtos);
    }
}
