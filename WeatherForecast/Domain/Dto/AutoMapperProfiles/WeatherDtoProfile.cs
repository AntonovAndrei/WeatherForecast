using AutoMapper;
using WeatherForecast.Domain.Entities;

namespace WeatherForecast.Domain.Dto.AutoMapperProfiles
{
    public class WeatherDtoProfile : Profile
    {
        public WeatherDtoProfile()
        {
            CreateMap<Weather, WeatherDto>().ReverseMap();
        }
    }
}
