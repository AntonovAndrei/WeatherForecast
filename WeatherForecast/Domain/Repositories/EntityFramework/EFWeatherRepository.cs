using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Domain.Dto;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Repositories.Abstract;
using WeatherForecast.ViewModels;

namespace WeatherForecast.Domain.Repositories.EntityFramework
{
    public class EFWeatherRepository : IWeatherRepository
    {
        private readonly int _pageSize = 24;
        private readonly WeatherDbContext _context;
        private readonly IMapper _mapper;

        public EFWeatherRepository(WeatherDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddWeatherAsync(IList<WeatherDto> weatherDtos)
        {
            await _context.AddRangeAsync(_mapper.Map<IEnumerable<WeatherDto>, IEnumerable<Weather>>(weatherDtos));
            await _context.SaveChangesAsync();
        }

        public async Task<WeatherListViewModel> GetWeatherByMonthWithPagingAsync(DateTime date, int weatherPage)
        {
            DateTime monthBeginning = new DateTime(date.Year, date.Month, 1);
            DateTime monthEnd = monthBeginning.AddMonths(1).AddDays(-1);

            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<Weather, WeatherDto>());

            IEnumerable<WeatherDto> weathers = _context.Weathers
                .Where(d => d.Date >= monthBeginning && d.Date <= monthEnd)
                .OrderBy(d => d.Date)
                .Skip((weatherPage - 1) * _pageSize)
                .Take(_pageSize)
                .ProjectTo<WeatherDto>(configuration);

            int itemsCount = _context.Weathers
                .Where(d => d.Date >= monthBeginning && d.Date <= monthEnd)
                .Count();

            WeatherListViewModel weathesrList = new WeatherListViewModel
            {
                Weathers = _mapper.Map<IEnumerable<WeatherDto>, IEnumerable<WeatherViewModel>>(weathers),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = weatherPage,
                    ItemsPerPage = _pageSize,
                    TotalItems = itemsCount,
                    SearchDate = monthBeginning
                }
            };

            return weathesrList;
        }

        public async Task<IQueryable<WeatherDto>> GetWeatherByMonthAsync(DateTime date)
        {
            DateTime monthBeginning = new DateTime(date.Year, date.Month, 1);
            DateTime monthEnd = monthBeginning.AddMonths(1).AddDays(-1);

            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<Weather, WeatherDto>());
            
            IQueryable<WeatherDto> weathers = _context.Weathers
                .Where(d => d.Date >= monthBeginning && d.Date <= monthEnd)
                .OrderBy(d => d.Date)
                .ProjectTo<WeatherDto>(configuration);

            return weathers;
        }
    }
}
