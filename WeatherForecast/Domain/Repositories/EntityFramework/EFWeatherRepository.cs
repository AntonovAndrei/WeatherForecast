using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<WeatherDto>> GetWeatherByMonthWithPagingAsync(DateTime date,  int page)
        {
            int pageSize = 24;

            DateTime monthBeginning = new DateTime(date.Year, date.Month, 1);
            DateTime monthEnd = monthBeginning.AddMonths(1).AddDays(-1);

            IEnumerable<Weather> weathers = await _context.Weathers
                .Where(d => d.Date >= monthBeginning && d.Date <= monthEnd)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Weather>, IEnumerable<WeatherDto>>(weathers);
        }
    }
}
