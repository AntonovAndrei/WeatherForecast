using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Domain.Dto;
using WeatherForecast.ViewModels;

namespace WeatherForecast.Domain.Repositories.Abstract
{
    public interface IWeatherRepository
    {
        public Task<WeatherListModel> GetWeatherByMonthWithPagingAsync(DateTime date, int weatherPage, string search);
        public Task<WeatherListModel> GetWeatherByYearWithPagingAsync(DateTime date, int weatherPage, string search);
        public Task AddWeatherAsync(IList<WeatherDto> weatherDtos);
    }
}
