using AutoMapper;
using WeatherForecast.Domain.Dto;

namespace WeatherForecast.ViewModels.AutoMapperProfiles
{
    public class WeatherViewModelProfile : Profile
    {
        public WeatherViewModelProfile()
        {
            CreateMap<WeatherDto, WeatherViewModel>().ReverseMap();
        }
    }
}
