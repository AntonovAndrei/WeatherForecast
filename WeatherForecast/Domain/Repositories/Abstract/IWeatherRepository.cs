using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Domain.Dto;
using WeatherForecast.ViewModels;

namespace WeatherForecast.Domain.Repositories.Abstract
{
    public interface IWeatherRepository
    {
        public Task<WeatherListViewModel> GetWeatherByMonthWithPagingAsync(DateTime date, int weatherPage);
        public Task<IQueryable<WeatherDto>> GetWeatherByMonthAsync(DateTime date);
        public Task AddWeatherAsync(IList<WeatherDto> weatherDtos);
    }
}
