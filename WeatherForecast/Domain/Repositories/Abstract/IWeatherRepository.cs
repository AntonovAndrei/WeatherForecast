using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Domain.Dto;

namespace WeatherForecast.Domain.Repositories.Abstract
{
    public interface IWeatherRepository
    {
        public Task<IEnumerable<WeatherDto>> GetWeatherByMonthWithPagingAsync(DateTime date, int page);
        public Task AddWeatherAsync(IEnumerable<WeatherDto> weatherDtos);
    }
}
