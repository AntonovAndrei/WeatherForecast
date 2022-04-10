using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Domain.Dto;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Repositories.Abstract;

namespace WeatherForecast.Domain.Repositories.EntityFramework
{
    public class EFWeatherRepository : IWeatherRepository
    {
        private readonly WeatherDbContext _context;
        private readonly IMapper _mapper;

        public EFWeatherRepository(WeatherDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddWeatherAsync(IEnumerable<WeatherDto> weatherDtos)
        {
            await _context.AddRangeAsync(_mapper.Map<IEnumerable<WeatherDto>, IEnumerable<Weather>>(weatherDtos));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WeatherDto>> GetWeatherByWeekAsync(DateTime monthBeginning)
        {
            throw new NotImplementedException();
        }
    }
}
